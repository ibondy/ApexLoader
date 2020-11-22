#region Copyright

// MIT License
// 
// Copyright (c) 2020 Ivan Bondy
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

#endregion



// ReSharper disable AsyncConverter.AsyncWait

namespace ApexLoader
{
    #region using

    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Timers;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Raven.Client.Exceptions;
    using Timer = System.Timers.Timer;

    #endregion

    public class LoaderService : IHostedService, IDisposable
    {
        private readonly ApexConfigs _apexConfigs;
        private readonly ApexService _apexService;
        private readonly IConfiguration _configuration;
        private readonly CookieDelegateHandler _cookieDelegateHandler;
        private readonly IDbClient _dbClient;
        private readonly ILogger<LoaderService> _logger;
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();
        private Timer _timer;

        public LoaderService(IConfiguration configuration, ApexService apexService, CookieDelegateHandler cookieDelegateHandler, ApexConfigs configs, ILogger<LoaderService> logger, IDbClient dbClient)
        {
            _configuration = configuration;
            _apexService = apexService;
            _cookieDelegateHandler = cookieDelegateHandler;
            _apexConfigs = configs;
            _logger = logger;
            _dbClient = dbClient;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        /// <summary>
        ///     Triggered when the application host is ready to start the service.
        /// </summary>
        /// <param name="cancellationToken"> Indicates that the start process has been aborted. </param>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting LoaderService");
            _timer = new Timer();
            _timer.Elapsed += OnElapsed;
            var interval = Convert.ToDouble(_configuration["DownloadInterval"]);
            _timer.Interval = TimeSpan.FromMinutes(interval).TotalMilliseconds;
            _timer.Start();

            await DoWork(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        ///     Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        /// <param name="cancellationToken"> Indicates that the shutdown process should no longer be graceful. </param>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping LoaderService");
            _timer.Stop();
            return Task.CompletedTask;
        }

        private async Task ApexExecuteAsync(string apexName, CancellationToken stoppingToken)
        {
            var sw = new Stopwatch();
            sw.Start();

            _logger.LogInformation($"Executing {apexName} download");

            switch (apexName)
            {
                case "Apex1":
                    if (_apexConfigs.Apex1.Active)
                    {
                        _apexService.SetContext(_apexConfigs.Apex1);
                    }
                    else
                    {
                        return;
                    }

                    break;

                case "Apex2":
                    if (_apexConfigs.Apex2.Active)
                    {
                        _apexService.SetContext(_apexConfigs.Apex2);
                    }
                    else
                    {
                        return;
                    }

                    break;
            }

            _apexService.GetLogin().Wait();

            if (!string.IsNullOrEmpty(_cookieDelegateHandler.AccessCookie))
            {
                var tlog = await _apexService.GetTLog().ConfigureAwait(false);
                var dlog = await _apexService.GetDLog().ConfigureAwait(false);
                var olog = await _apexService.GetOLog().ConfigureAwait(false);

                var log = await _apexService.GetLog().ConfigureAwait(false);
                var status = await _apexService.GetStatus().ConfigureAwait(false);
                var config = await _apexService.GetConfig().ConfigureAwait(false);

                if (config != null) //Apex configuration
                {
                    config.ApexId = apexName;
                    try
                    {
                        await _dbClient.AddConfig(config).ConfigureAwait(false);
                    }
                    catch (RavenException ex)
                    {
                        _logger.LogError(ex, ex.Message);
                        _logger.LogInformation($"{apexName} unsuccessful. Unable to connect to database");
                        return;
                    }
                }

                if (status != null) //Apex status
                {
                    status.ApexId = apexName;
                    try
                    {
                        await _dbClient.AddStatus(status).ConfigureAwait(false);
                    }
                    catch (RavenException ex)
                    {
                        _logger.LogError(ex, ex.Message);
                        _logger.LogInformation($"{apexName} unsuccessful. Unable to connect to database");
                        return;
                    }
                }

                if (log != null) //Apex log
                {
                    log.ApexId = apexName;
                    try
                    {
                        await _dbClient.AddRecords(log.Ilog.Record, log.ApexId, log.Ilog.Timezone).ConfigureAwait(false);
                        await _dbClient.AddLog(log).ConfigureAwait(false);
                    }
                    catch (RavenException ex)
                    {
                        _logger.LogError(ex, ex.Message);
                        _logger.LogInformation($"{apexName} unsuccessful. Unable to connect to database");
                        return;
                    }
                }

                if (tlog != null && tlog.Tlog.Record.Any()) //Apex Trident
                {
                    try
                    {
                        await _dbClient.AddTRecords(tlog.Tlog.Record, apexName, tlog.Tlog.Timezone);
                    }
                    catch (RavenException ex)
                    {
                        _logger.LogError(ex, ex.Message);
                        _logger.LogInformation($"{apexName} unsuccessful. Unable to connect to database");
                        return;
                    }
                }

                if (olog != null) //Apex outlets
                {
                    try
                    {
                        await _dbClient.AddORecords(olog.Olog.Record, apexName, olog.Olog.Timezone);
                    }
                    catch (RavenException ex)
                    {
                        _logger.LogError(ex, ex.Message);
                        _logger.LogInformation($"{apexName} unsuccessful. Unable to connect to database");
                        return;
                    }
                }
            }

            _logger.LogInformation($"Completed Apex1 download in {sw.Elapsed.TotalSeconds} seconds");
        }

        private async Task DoWork(CancellationToken cancellationToken)
        {
            await ApexExecuteAsync("Apex1", cancellationToken).ConfigureAwait(false);
            await ApexExecuteAsync("Apex2", cancellationToken).ConfigureAwait(false);
        }

        private async void OnElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                _logger.LogInformation("Starting new Apex download from timer");
                await DoWork(_stoppingCts.Token).ConfigureAwait(false);
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, "OnElapsed - likely due to PC going to sleep");
            }
        }
    }
}