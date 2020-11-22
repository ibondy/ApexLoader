using ApexLoader.ApexConfig;
using ApexLoader.ApexLog;
using ApexLoader.ApexStatus;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApexLoader
{
    public interface IDbClient
    {
        Task AddConfig(ConfigRoot configRoot);

        Task AddLog(LogRoot logRoot);

        Task AddTRecords(IList<ApexTLog.Record> records, string apexId, string timeZoneOffset);

        Task AddORecords(IList<ApexOlog.Record> records, string apexId, string timeZoneOffset);

        Task AddRecords(IList<Record> records, string apexId, string timeZoneOffset);

        Task AddStatus(StatusRoot statusRoot);

        void Dispose();
    }
}