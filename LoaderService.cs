using System;
using System.Threading.Tasks;

// ReSharper disable AsyncConverter.AsyncWait

namespace ApexLoader
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    using Raven.Client.Exceptions;

    using System.Diagnostics;
    using System.Threading;

    public class LoaderService : IHostedService, IDisposable
    {
        private readonly ApexConfigs _apexConfigs;
        private readonly ApexService _apexService;
        private readonly IConfiguration _configuration;
        private readonly CookieDelegateHandler _cookieDelegateHandler;
        private readonly IDbClient _dbClient;
        private readonly ILogger<LoaderService> _logger;
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();
        private System.Timers.Timer _timer;

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
                var log = await _apexService.GetLog().ConfigureAwait(false);
                var status = await _apexService.GetStatus().ConfigureAwait(false);
                var config = await _apexService.GetConfig().ConfigureAwait(false);

                if (config != null)
                {
                    config.ApexId = apexName;
                    try
                    {
                        await _dbClient.AddConfig(config).ConfigureAwait(false);
                    }
                    catch (RavenException ex)
                    {
                        _logger.LogError(ex, ex.Message);
                        _logger.LogInformation($"{apexName} unsucessfull. Unable to connect to database");
                        return;
                    }
                }

                if (status != null)
                {
                    status.ApexId = apexName;
                    try
                    {
                        await _dbClient.AddStatus(status).ConfigureAwait(false);
                    }
                    catch (RavenException ex)
                    {
                        _logger.LogError(ex, ex.Message);
                        _logger.LogInformation($"{apexName} unsucessfull. Unable to connect to database");
                        return;
                    }
                }

                if (log != null)
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
                        _logger.LogInformation($"{apexName} unsucessfull. Unable to connect to database");
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

        private async void OnElapsed(object sender, System.Timers.ElapsedEventArgs e)
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
        /// Triggered when the application host is ready to start the service.
        /// </summary>
        /// <param name="cancellationToken"> Indicates that the start process has been aborted. </param>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting LoaderService");
            _timer = new System.Timers.Timer();
            _timer.Elapsed += OnElapsed;
            var interval = Convert.ToDouble(_configuration["DownloadInterval"]);
            _timer.Interval = TimeSpan.FromMinutes(interval).TotalMilliseconds;
            _timer.Start();

            await DoWork(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        /// <param name="cancellationToken"> Indicates that the shutdown process should no longer be graceful. </param>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping LoaderService");
            _timer.Stop();
            return Task.CompletedTask;
        }
    }
}