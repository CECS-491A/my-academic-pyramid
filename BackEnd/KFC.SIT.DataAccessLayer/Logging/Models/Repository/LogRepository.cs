using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Models;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;



namespace DataAccessLayer.Logging
{
    public class LogRepository<T> : ILogRepository<T>
    {
        MongoClient _client = new MongoClient("mongodb+srv://super:superheroes@myacademicpyramidlogging-if0cx.mongodb.net/test?retryWrites=true");
        IMongoDatabase _db; //= _client.GetDatabase("test");

        private IMongoCollection<T> _logCollection;

        public LogRepository(string collectionName)
        {
            _db = _client.GetDatabase("tes");
            _logCollection = _db.GetCollection<T>(collectionName);
        }

        public void CreateLog(T log)
        {
            _logCollection.InsertOneAsync(log);
        }

        // Get a Log by id
        public T GetLog(string logId)
        {
            return _logCollection.Find(new BsonDocument
            {
                {
                    "_id", new ObjectId(logId)
                }
            }).FirstAsync().Result;
        }

        public List<T> GetAllLogs()
        {
            return _logCollection.Find(new BsonDocument())
                .ToListAsync().Result;
        }

        public void DeleteLog(string logId)
        {
            _logCollection.DeleteOneAsync(new BsonDocument
            {
                {
                    "_id", new ObjectId(logId)
                }
            });
        }

        public void DeleteAllLogsForUser(string accountName)
        {
            _logCollection.DeleteManyAsync(new BsonDocument
            {
                {
                    "Account", new ObjectId(accountName)
                }
            });
        }

        public void DeleteAllLogs()
        {
            _logCollection.DeleteManyAsync(FilterDefinition<T>.Empty);
        }

        public async Task<long> CountLogs()
        {
            long count = await _logCollection.EstimatedDocumentCountAsync();
            return count;
            
        }
    }
}
