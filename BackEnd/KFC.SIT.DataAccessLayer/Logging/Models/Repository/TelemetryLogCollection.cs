using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Models;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;



namespace DataAccessLayer.Logging
{
    public class TelemetryLogCollection
    {
        MongoDBRepo _repo = new MongoDBRepo("mongodb+srv://super:superheroes@myacademicpyramidlogging-if0cx.mongodb.net/test?retryWrites=true", "test");

        private const string _collectionName = "TelemetryLogs";

        private readonly IMongoCollection<TelemetryLog> Collection;

        public TelemetryLogCollection()
        {
            this.Collection = _repo.Db.GetCollection<TelemetryLog>(_collectionName);
        }

        public List<TelemetryLog> GetAll()
        {
            var query = this.Collection.Find(new BsonDocument()).ToListAsync();
            return query.Result;
        }

        // Get a telemetryLog by id
        public TelemetryLog Get(string id)
        {
            return this.Collection.Find(new BsonDocument { { "_id", new ObjectId(id) } }).FirstAsync().Result;
        }

        public void DeleteAll()
        {
            this.Collection.DeleteManyAsync(FilterDefinition<TelemetryLog>.Empty);
        }

        public long CountLogs()
        {
            return this.Collection.EstimatedDocumentCount();
        }
    }
}
