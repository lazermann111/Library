using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;

namespace Library3.Helpers
{
    public static class MongoSessionManager
    {

        private static IMongoClient _client;
        private static IMongoDatabase _database;

        static MongoSessionManager()
        {
            _client = new MongoClient("mongodb://localhost:27017");
            _database = _client.GetDatabase("local");
           // _database = server.GetDatabase(;
        }


        public static IMongoClient Client => _client;
        public static IMongoDatabase Database => _database;
    }
}