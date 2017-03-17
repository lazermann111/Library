using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library3.Models
{
    public class Author
    {
     //   [BsonId]
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }

        // for mongo
        public ICollection<string> BookIds { get; set; }
    }
}