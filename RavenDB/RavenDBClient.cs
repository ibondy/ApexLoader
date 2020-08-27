using ApexLoader.ApexConfig;
using ApexLoader.ApexLog;
using ApexLoader.ApexStatus;

using Raven.Client;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexLoader.RavenDB
{
    public class RavenDBClient : IDbClient
    {
        private static Lazy<IDocumentStore> store = new Lazy<IDocumentStore>(CreateStore);
       public static IDocumentStore Store => store.Value;

        private static IDocumentStore CreateStore()
        {
            IDocumentStore store = new DocumentStore
            {
                Urls = new[] { "http://127.0.0.1:8080" },
                Database = "ApexClient"
            }.Initialize();

            return store;
        }
        public async Task AddConfig(ConfigRoot configRoot)
        {
            using (IAsyncDocumentSession session = Store.OpenAsyncSession("ApexClient"))
            {
                await session.StoreAsync(configRoot,"ConfigRoot/"+ configRoot.ApexId);
                await session.SaveChangesAsync();
            }
        }

        public async Task AddLog(LogRoot logRoot)
        {
            using (IAsyncDocumentSession session = Store.OpenAsyncSession("ApexClient"))
            {
                await session.StoreAsync(logRoot,"logRoot/"+ logRoot.ApexId);
                await session.SaveChangesAsync();
            }
        }

        public async Task AddRecords(IList<Record> records, string apexId, string timeZoneOffset)
        {
            DateTime lastEntry = DateTime.Now.AddDays(-1);
            int tzOffset = Int32.Parse(Decimal.Parse(timeZoneOffset).ToString("#"));
            using (IAsyncDocumentSession session = Store.OpenAsyncSession("ApexClient"))
            {
                try
                {
                    var result = await session.Query<RecordLog>().Where(p => p.ApexId == apexId).OrderByDescending(p => p.DateTime).FirstAsync();
                    if (result != null)
                    {
                        lastEntry = result.DateTime;
                    }                
                }
                catch(InvalidOperationException ex)
                {                   
                    //expected when there are no RecordLog entries
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


                await session.SaveChangesAsync();
            }
           
        }

        public async Task AddStatus(StatusRoot statusRoot)
        {
            using (IAsyncDocumentSession session = Store.OpenAsyncSession("ApexClient"))
            {
                await session.StoreAsync(statusRoot, "StatusRoot/"+ statusRoot.ApexId);
                await session.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
            Store.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
