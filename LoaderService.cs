using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable AsyncConverter.AsyncWait

namespace ApexLoader
{
    using System.Diagnostics;
    using System.Net;
    using System.Threading;
    using System.Xml;

    using ApexLoader.Cosmos;
    using RavenDB;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    public class LoaderService :IHostedService
    {
        private readonly IConfiguration _configuration;
        private readonly ApexService _apexService1;
        private readonly ApexService _apexService2;
        private readonly CookieDelegateHandler _cookieDelegateHandler;
        private readonly ApexConfigs _apexConfigs;
        private readonly ILogger<LoaderService> _logger;
        private readonly CosmosClient _cosmosClient;
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();
       

        public LoaderService(IConfiguration configuration, ApexService apexService1, ApexService apexService2,  CookieDelegateHandler cookieDelegateHandler, ApexConfigs configs, ILogger<LoaderService> logger, CosmosClient cosmosClient)
        {
            _configuration = configuration;
            _apexService1 = apexService1;
            _apexService2 = apexService2;
            _cookieDelegateHandler = cookieDelegateHandler;
            _apexConfigs = configs;
            _logger = logger;
            _cosmosClient = cosmosClient;
        }
        /// <summary>
        /// Triggered when the application host is ready to start the service.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await Apex1ExecuteAsync(cancellationToken);
            await Apex2ExecuteAsync(cancellationToken);

           
        }

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the shutdown process should no longer be graceful.</param>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async Task Apex1ExecuteAsync(CancellationToken stoppingToken)
        {
            Debug.WriteLine("Executing Apex1");
            if (_apexConfigs.Apex1.Active)
            {
                _apexService1.SetContext(_apexConfigs.Apex1);
                _apexService1.GetLogin().Wait();

                 if (!string.IsNullOrEmpty(_cookieDelegateHandler.AccessCookie))
                 {
                    var log = await _apexService1.GetLog().ConfigureAwait(false);
                    log.ApexId = "Apex1";
                    
                   // await _cosmosClient.AddRecords(log.Ilog.Record, log.ApexId, log.Ilog.Timezone);
                   // await _cosmosClient.AddLog(log);
                                      
                    var status = await _apexService1.GetStatus().ConfigureAwait(false);
                    status.ApexId = "Apex1";
                   // await _cosmosClient.AddStatus(status);
                    
                    var config = await _apexService1.GetConfig().ConfigureAwait(false);
                    config.ApexId = "Apex1";
                   // await _cosmosClient.AddConfig(config);

                    var raven = new RavenDBClient();
                    await raven.AddConfig(config);
                    await raven.AddLog(log);
                    await raven.AddStatus(status);
                    await raven.AddRecords(log.Ilog.Record, log.ApexId, log.Ilog.Timezone);

                 }
                
            }
            Debug.WriteLine("Completed Apex1");
        }

        private async Task Apex2ExecuteAsync(CancellationToken stoppingToken)
        {
            Debug.WriteLine("Executing Apex2");

            if (_apexConfigs.Apex2.Active)
            {
                _apexService2.SetContext(_apexConfigs.Apex2);
                _apexService2.GetLogin().Wait();

                if (!string.IsNullOrEmpty(_cookieDelegateHandler.AccessCookie))
                {
                    var log = await _apexService2.GetLog().ConfigureAwait(false);
                    log.ApexId = "Apex2";

                    //await _cosmosClient.AddRecords(log.Ilog.Record, log.ApexId, log.Ilog.Timezone);
                    //await _cosmosClient.AddLog(log);

                    var status = await _apexService2.GetStatus().ConfigureAwait(false);
                    status.ApexId = "Apex2";
                    //await _cosmosClient.AddStatus(status);
                    var config = await _apexService2.GetConfig().ConfigureAwait(false);
                    config.ApexId = "Apex2";
                    //await _cosmosClient.AddConfig(config);

                    var raven = new RavenDBClient();
                    await raven.AddConfig(config);
                    await raven.AddLog(log);
                    await raven.AddStatus(status);
                    await raven.AddRecords(log.Ilog.Record, log.ApexId, log.Ilog.Timezone);
                }
            }
            Debug.WriteLine("Completed Apex2");
        }
    }
}
