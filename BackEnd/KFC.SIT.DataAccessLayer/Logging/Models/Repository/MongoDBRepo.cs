using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace DataAccessLayer.Logging
{
    public class MongoDBRepo
    {
        public MongoClient Client;
        public IMongoDatabase Db;

        public MongoDBRepo(string url, string database)
        {
            this.Client = new MongoClient(url);
            // Comment out. (create database if it doenst exist already)
            this.Db = this.Client.GetDatabase(database);
        }
    }
}
