
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using Library3.Models;
using Library3.DTO;
using Library3.Entities.Mongo;
using Library3.Helpers;

namespace Library3.Repositories
{
    public class MongoBookRepository : IBookrepository
    {
      
        IMongoCollection<MongoBook> _books;

        public MongoBookRepository()
        {
            _books = MongoSessionManager.Database.GetCollection<MongoBook>("Books");
        }

        public BookDto Get(string id)
        {
            //var q = Query.EQ("_id", id);
            var book = _books.Find(q => q.Id == id).FirstOrDefault().Map();
            return book;
        }

        public IEnumerable<BookDto> GetAll()
        {
            var cursor = _books.Find(_ => true).ToList();
            var c = cursor.ToList().Select(book => book.Map());
            
            return c;
        }

        public void Remove(string id)
        {
           // var query = Query.EQ("_id", id);
            _books.DeleteOne(b => b.Id == id);
        }

        public void Add(string name, string authorId)
        {
            var authors = MongoSessionManager.Database.GetCollection<MongoAuthor>("Authors");
            var author = authors.Find(b => b.Id == authorId).FirstOrDefault();

            var item = new MongoBook
            {
                Name = name,
                AuthorId =
                    new MongoDBRef("Authors", author.Id),
                Id = ObjectId.GenerateNewId().ToString()
            };
            _books.InsertOne(item);
            
        }

        public bool Update(string id, string name, string authorId)
        {
            var item = _books.Find(b => b.Id == id).FirstOrDefault();
            if (item == null) return false; 
            item.Name = name;

            _books.ReplaceOne(av => av.Id == id, item);

            return true;
        }
    }
}
