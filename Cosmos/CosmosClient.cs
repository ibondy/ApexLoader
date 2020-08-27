using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using ApexLoader.ApexConfig;
using Microsoft.AspNetCore.Http.Connections.Client;
using ApexLoader.ApexStatus;
using ApexLoader.ApexLog;
using Microsoft.Azure.Cosmos.Linq;

namespace ApexLoader.Cosmos
{
    public class CosmosClient : IDisposable, IDbClient
    {
        private readonly ILogger<CosmosClient> _logger;
        private string _primaryKey;
        private string _endpoint;
        private Microsoft.Azure.Cosmos.CosmosClient _cosmosClient;
        private const string APEXDATABASE = "ApexClient";
        private const string APEXCONFIG = "ApexConfig";
        private const string APEXLOG = "ApexLog";
        private const string APEXRECORD = "ApexRecord";
        private const string APEXSTATUS = "ApexStatus";
        private const string APEXID = "/ApexId";

        public CosmosClient(IConfiguration configuration, ILogger<CosmosClient> logger)
        {
            _endpoint = configuration["Cosmos.Endpoint"];
            _primaryKey = configuration["Cosmos.PrimaryKey"];
            _logger = logger;
            _cosmosClient = new Microsoft.Azure.Cosmos.CosmosClient(_endpoint, _primaryKey, new CosmosClientOptions { ConnectionMode = ConnectionMode.Direct });
            _ = InitializeDatabase();
        }

        private async Task InitializeDatabase()
        {
            var result = await _cosmosClient.CreateDatabaseIfNotExistsAsync(APEXDATABASE);
            if (result.StatusCode == System.Net.HttpStatusCode.Created)
            {
                var taskList = new List<Task>
                {
                    result.Database.CreateContainerIfNotExistsAsync(APEXCONFIG, APEXID),
                    result.Database.CreateContainerIfNotExistsAsync(APEXLOG, APEXID),
                    result.Database.CreateContainerIfNotExistsAsync(APEXSTATUS, APEXID),
                    result.Database.CreateContainerIfNotExistsAsync(APEXRECORD, APEXID)
                };
                await Task.WhenAll();
            }
        }

        public async Task AddConfig(ConfigRoot configRoot)
        {
            var container = _cosmosClient.GetContainer(APEXDATABASE, APEXCONFIG);
            try
            {
                await container.UpsertItemAsync<ConfigRoot>(configRoot);
            }
            catch (CosmosException ex)
            {
                _logger.LogError(ex.Message);
                await container.CreateItemAsync<ConfigRoot>(configRoot);
                _logger.LogInformation("Initial ConfigRoot entry successfully created");
            }
        }

        public async Task AddStatus(StatusRoot statusRoot)
        {
            var container = _cosmosClient.GetContainer(APEXDATABASE, APEXSTATUS);
            try
            {
                await container.UpsertItemAsync<StatusRoot>(statusRoot);
            }
            catch (CosmosException ex)
            {
                _logger.LogError(ex.Message);
                await container.CreateItemAsync<StatusRoot>(statusRoot);
                _logger.LogInformation("Initial StatusRoot entry successfully created");
            }
        }

        public async Task AddLog(LogRoot logRoot)
        {
            var container = _cosmosClient.GetContainer(APEXDATABASE, APEXLOG);
            try
            {
                logRoot.Ilog.Record = null;
                await container.UpsertItemAsync<LogRoot>(logRoot);
            }
            catch (CosmosException ex)
            {
                _logger.LogError(ex.Message);
                await container.CreateItemAsync<LogRoot>(logRoot);
                _logger.LogInformation("Initial LogRoot entry successfully created");
            }
        }

        public async Task AddRecords(IList<Record> records, string apexId, string timeZoneOffset)
        {
            int tzOffset = Int32.Parse(Decimal.Parse(timeZoneOffset).ToString("#"));
            var container = _cosmosClient.GetContainer(APEXDATABASE, APEXRECORD);
            var query = container.GetItemLinqQueryable<RecordLog>(true, requestOptions: new QueryRequestOptions { MaxItemCount = 1 }).OrderByDescending(p => p.DateTime).Where(p => p.ApexId == apexId);
            var result = container.GetItemQueryIterator<RecordLog>(query.ToQueryDefinition(), requestOptions: new QueryRequestOptions { MaxItemCount = 1 });
            DateTime lastEntry = DateTime.Today.AddDays(-2);
            while (result.HasMoreResults)
            {
                var x = await result.ReadNextAsync();
                if (x.Any())
                {
                    lastEntry = x.First().DateTime;
                }

                break;
            }

            foreach (var record in records)
            {
                foreach (var datum in record.Data)
                {
                    var dateStamp = DateTimeOffset.FromUnixTimeSeconds(record.Date).ToOffset(new TimeSpan(tzOffset, 0, 0)).DateTime;
                    if (dateStamp > lastEntry)
                    {
                        var recordLog = new RecordLog(dateStamp, apexId, datum);
                        await container.CreateItemAsync<RecordLog>(recordLog);
                    }

                }

            }


        }

        public void Dispose()
        {
            _cosmosClient.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
