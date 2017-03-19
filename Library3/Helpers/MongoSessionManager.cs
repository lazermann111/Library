using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;

namespace Library3.Helpers
{
    public static class MongoSessionManager
    {

        private static MongoClient _client;
        private static MongoDatabase _database;

        static MongoSessionManager()
        {
            _client = new MongoClient("mongodb://localhost:27017");
            var server = _client.GetServer();
            _database = server.GetDatabase("local");
        }


        public static MongoClient Client => _client;
        public static MongoDatabase Database => _database;
    }
}