using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library3.Postgres
{
    public class PostgresBook
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }   
        public virtual PostgresAuthor Author { get; set; }
    }
}