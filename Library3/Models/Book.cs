using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library3.Models
{
    public class Book
    {
        //  [BsonId]
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }   
        public virtual Author Author { get; set; }

        //for mongo
        public virtual string AuthorId { get; set; }
    }
}