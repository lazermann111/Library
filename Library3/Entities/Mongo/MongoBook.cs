using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Library3.Entities.Mongo
{
    public class MongoBook
    {
        [BsonId]
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }

      //  [Bson]
        public virtual MongoDBRef AuthorId { get; set; }
    }
}