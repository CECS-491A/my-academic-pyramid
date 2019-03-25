using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Models;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;



namespace DataAccessLayer.Logging
{
    public class ErrorLogCollection
    {
        MongoDBRepo _repo = new MongoDBRepo("mongodb+srv://super:superheroes@myacademicpyramidlogging-if0cx.mongodb.net/test?retryWrites=true", "test");

        private const string _collectionName = "ErrorLogs";

        private readonly IMongoCollection<ErrorLog> Collection;

        public ErrorLogCollection()
        {
            this.Collection = _repo.Db.GetCollection<ErrorLog>(_collectionName);
        }

        public List<ErrorLog> GetAll()
        {
            var query = this.Collection.Find(new BsonDocument()).ToListAsync();
            return query.Result;
        }

        // Get an ErrorLog by id
        public ErrorLog Get(string id)
        {
            return this.Collection.Find(new BsonDocument { { "_id", new ObjectId(id) } }).FirstAsync().Result;
        }

        public void Delete(string id)
        {
            this.Collection.FindOneAndDeleteAsync(new BsonDocument { { "_id", new ObjectId(id) } });
        }

        public void DeleteAll()
        {
            this.Collection.DeleteManyAsync(FilterDefinition<ErrorLog>.Empty);
        }

        public long CountLogs()
        {
            return this.Collection.EstimatedDocumentCount();
        }
    }
}
