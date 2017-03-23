using MongoDB.Bson;
using MongoDB.Driver;
using Library3.Models;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Library3.Repositories;
using Library3.DTO;
using Library3.Entities.Mongo;
using Library3.Helpers;
using Library3.Repositories.Async;

namespace Library3.Repositories
{
    public class MongoAuthorRepository : IAuthorRepository
    {

        
        IMongoCollection<MongoAuthor> _authors;

        public MongoAuthorRepository()
        {
            _authors = MongoSessionManager.Database.GetCollection<MongoAuthor>("Authors");
        }


        public IEnumerable<AuthorDto> GetAll(int page)
        {
            var cursor = _authors.Find(_ => true);
            var dto = cursor.ToList().OrderBy(a => a.Name).Skip(page * 10).Take(10).Select(d => d.Map());
            return dto;
        }

        public AuthorDto Get(string id)
        {
          // var q = Query.EQ("_id", id);
           var author = _authors.Find(a => a.Id == id).FirstOrDefault()?.Map();
           
           return author;
        }

       

        public void Remove(string id)
        {
            //var query = Query.EQ("_id", id);
            _authors.DeleteOne(b => b.Id == id);
        }

        public bool Update(string authorId, string name)
        {
           // var item = _authors.FindOne(Query.EQ("_id", authorId));
            var item = _authors.Find(a => a.Id == authorId).FirstOrDefault();
            if (item == null) return false;

            item.Name = name;
            _authors.ReplaceOne(a => a.Id == authorId, item);

            return true;
        }

        public void Add(string name)
        {
            MongoAuthor b = new MongoAuthor
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = $"author{name}",
            };
            _authors.InsertOne(b);
        }
    }
}