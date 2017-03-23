using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library3.Entities.Mongo
{
    public class MongoEntity
    {

        [BsonId]
        public virtual string Id { get; set; }
    }
}