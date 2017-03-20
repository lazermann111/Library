using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Library3.Entities.Mongo
{
    public class MongoAuthor
    {
        [BsonId]
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IEnumerable<MongoDBRef> BookIds { get; set; }
    }
}