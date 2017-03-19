
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
      
        MongoCollection<MongoBook> _books;

        public MongoBookRepository()
        {
            _books = MongoSessionManager.Database.GetCollection<MongoBook>("Books");
        }

        public BookDto Get(string id)
        {
            var q = Query.EQ("_id", id);
            var book = _books.FindOne(q).Map();
           /* var author =
                MongoSessionManager.Database.GetCollection<MongoAuthor>("Authors")
                    .FindOne(Query.EQ("_id", book.AuthorId));
            book.AuthorId = author.Id;
            */
           // var dto = AutoMapper.Mapper.Map<BookDto>(book);
            //dto.Author = AutoMapper.Mapper.Map<AuthorDto>(author);
            return book;
        }

        public IEnumerable<BookDto> GetAll()
        {
            MongoCursor<MongoBook> cursor = _books.FindAll();
            var c = cursor.ToList().Select(book => book.Map());
            
            return c;
        }

        public void Remove(string id)
        {
            var query = Query.EQ("_id", id);
            _books.Remove(query);
        }

        public void Add(string name, string authorId)
        {
            var authors = MongoSessionManager.Database.GetCollection<MongoAuthor>("Authors");
            var author = authors.FindOne(Query.EQ("_id", authorId));

            var item = new MongoBook
            {
                Name = name,
                AuthorId =
                    new MongoDBRef(MongoSessionManager.Database.GetCollection<MongoAuthor>("Authors").Name, author.Id),
                Id = ObjectId.GenerateNewId().ToString()
            };
            _books.Insert(item);
            
        }

        public bool Update(string id, string name, string authorId)
        {
            var item = _books.FindOne(Query.EQ("_id", id));
            if (item == null) return false;

            /*item.AuthorId = 
                MongoSessionManager.Database.GetCollection<MongoAuthor>("Authors").FindOne(Query.EQ("_id", authorId)).Id;*/
            item.Name = name;

            _books.Save(item);

            return true;
        }
    }
}
