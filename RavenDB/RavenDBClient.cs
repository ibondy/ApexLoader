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

namespace ApexLoader.RavenDB
{
    #region using

    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using ApexConfig;
    using ApexLog;
    using ApexStatus;
    using Microsoft.Extensions.Logging;
    using Raven.Client.Documents;
    using Raven.Client.Exceptions.Documents.Session;

    #endregion

    [SuppressMessage("ReSharper", "ArrangeThisQualifier")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class RavenDBClient : IDbClient
    {
        private static readonly Lazy<IDocumentStore> DBstore = new Lazy<IDocumentStore>(CreateStore);
        private static ApexConfigs _config;
        private readonly ILogger<RavenDBClient> _logger;

        public RavenDBClient(ILogger<RavenDBClient> logger)
        {
            _logger = logger;
            _config = config;
        }

        public static IDocumentStore Store => DBstore.Value;

        public async Task AddConfig(ConfigRoot configRoot)
        {
            using var session = Store.OpenAsyncSession("ApexClient");
            await session.StoreAsync(configRoot, "ConfigRoot/" + configRoot.ApexId);
            await session.SaveChangesAsync();
        }

        public async Task AddLog(LogRoot logRoot)
        {
            logRoot.Ilog.Record = null; // no need to store as we are using AddRecords
            using var session = Store.OpenAsyncSession("ApexClient");
            await session.StoreAsync(logRoot, "logRoot/" + logRoot.ApexId);
            await session.SaveChangesAsync();
        }

        public async Task AddRecords(IList<Record> records, string apexId, string timeZoneOffset)
        {
            var lastEntry = DateTime.Now.AddDays(-1);
            var tzOffset = int.Parse(decimal.Parse(timeZoneOffset).ToString("#"));
            using var session = Store.OpenAsyncSession("ApexClient");
            try
            {
                var result = await session.Query<RecordLog>().Where(p => p.ApexId == apexId).OrderByDescending(p => p.DateTime).FirstAsync();
                if (result != null)
                {
                    lastEntry = result.DateTime;
                }
            }
            catch (InvalidOperationException ex)
            {
                //expected when there are no RecordLog entries
            }

            if (records == null)
            {
                return;
            }

            foreach (var record in records)
            {
                foreach (var datum in record.Data)
                {
                    var dateStamp = DateTimeOffset.FromUnixTimeSeconds(record.Date).ToOffset(new TimeSpan(tzOffset, 0, 0)).DateTime;
                    if (dateStamp > lastEntry)
                    {
                        var recordLog = new RecordLog(dateStamp, apexId, datum);
                        await session.StoreAsync(recordLog);
                    }
                }
            }

            // await session.SaveChangesAsync();
        }

        public async Task AddTRecords(IList<ApexTLog.Record> records, string apexId, string timeZoneOffset)
        {
            var lastEntry = DateTime.Now.AddDays(-1);
            var tzOffset = int.Parse(decimal.Parse(timeZoneOffset).ToString("#"));
            using var session = Store.OpenAsyncSession("ApexClient");
            try
            {
                var result = await session.Query<RecordLog>().Where(p => p.ApexId == apexId).OrderByDescending(p => p.DateTime).FirstAsync();
                if (result != null)
                {
                    lastEntry = result.DateTime;
                }
            }
            catch (InvalidOperationException ex)
            {
                //expected when there are no RecordLog entries
            }

            if (records == null)
            {
                return;
            }

            foreach (var record in records)
            {
                var datum = new Datum {Did = record.Did, Value = record.Value.ToString(CultureInfo.CurrentCulture)};
                if (record.Did.EndsWith("0"))
                {
                    datum.Type = "Alk";
                }

                if (record.Did.EndsWith("1"))
                {
                    datum.Type = "Cal";
                }

                if (record.Did.EndsWith("2"))
                {
                    datum.Type = "Mg";
                }

                datum.Name = $"{datum.Did}_{datum.Type}";

                var dateStamp = DateTimeOffset.FromUnixTimeSeconds(record.Date).ToOffset(new TimeSpan(tzOffset, 0, 0)).DateTime;
                if (dateStamp > lastEntry)
                {
                    var recordLog = new RecordLog(dateStamp, apexId, datum);
                    await session.StoreAsync(recordLog, $"RecordLogs/{datum.Did}:{record.Date}");
                }
            }

            await session.SaveChangesAsync();
        }

        public async Task AddORecords(IList<ApexOlog.Record> records, string apexId, string timeZoneOffset)
        {
            var lastEntry = DateTime.Now.AddDays(-1);
            var tzOffset = int.Parse(decimal.Parse(timeZoneOffset).ToString("#"));
            using var session = Store.OpenAsyncSession("ApexClient");
            try
            {
                var result = await session.Query<RecordLog>().Where(p => p.ApexId == apexId).OrderByDescending(p => p.DateTime).FirstAsync();
                if (result != null)
                {
                    lastEntry = result.DateTime;
                }
            }
            catch (InvalidOperationException ex)
            {
                //expected when there are no RecordLog entries
            }

            if (records == null)
            {
                return;
            }

            foreach (var record in records)
            {
                var dateStamp = DateTimeOffset.FromUnixTimeSeconds(record.Date).ToOffset(new TimeSpan(tzOffset, 0, 0)).DateTime;
                if (dateStamp > lastEntry)
                {
                    var datum = new Datum {Did = record.Did, Name = record.Name, Type = "pwr", Value = record.Value};
                    var recordLog = new RecordLog(dateStamp, apexId, datum)
                                    {
                                        Did = record.Did,
                                        Name = record.Name,
                                        Value = record.Value
                                    };
                    try
                    {
                        await session.StoreAsync(recordLog, $"RecordLogs/{datum.Did}:{record.Date}");
                    }
                    catch (NonUniqueObjectException ex)
                    {
                        _logger.LogError(ex, ex.Message);
                    }
                }
            }

            await session.SaveChangesAsync();
        }

        public async Task AddStatus(StatusRoot statusRoot)
        {
            using (var session = Store.OpenAsyncSession("ApexClient"))
            {
                await session.StoreAsync(statusRoot, "StatusRoot/" + statusRoot.ApexId);
                await session.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
            Store.Dispose();
            GC.SuppressFinalize(this);
        }

        private static IDocumentStore CreateStore()
        {
            var createStore = new DocumentStore
                              {
                                  Urls = new[] {"http://127.0.0.1:8080"},
                                  Database = "ApexClient"
                              }.Initialize();

            return createStore;
        }
    }
}