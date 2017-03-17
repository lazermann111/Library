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
        public string Id { get; set; }
        public string Name { get; set; }   
        public Author Author { get; set; }

        //for mongo
        public string AuthorId { get; set; }
    }
}