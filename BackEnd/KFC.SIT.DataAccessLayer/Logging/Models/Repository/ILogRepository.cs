using System.Collections.Generic;

namespace DataAccessLayer.Logging
{
    public interface ILogRepository<T>
    {
        void CreateLog(T createLog);
        T GetLog(string logId);
        List<T> GetAllLogs();
        void DeleteLog(string logId);
        void DeleteAllLogsForUser(string accountName);
        void DeleteAllLogs();
    }
}