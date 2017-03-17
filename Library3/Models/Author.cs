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
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; }

        // for mongo
       
        public virtual ICollection<string> BookIds { get; set; }
    }
}