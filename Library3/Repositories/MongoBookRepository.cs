
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using Library3.Models;
using Library3.DTO;

namespace Library3.Repositories
{
    public class MongoBookRepository : IBookrepository
    {
        MongoClient _client;
        MongoDatabase _database;
        MongoCollection<Book> _books;

        public MongoBookRepository()
        {


            _client = new MongoClient("mongodb://localhost:27017");
            var server = _client.GetServer();
            _database = server.GetDatabase("local");
            _books = _database.GetCollection<Book>("BookIds");


            /*_books.RemoveAll();
            for (int index = 1; index < 3; index++)
            {
               Book b = new Book
               {
                   Id = ObjectId.GenerateNewId().ToString(),
                   Name = string.Format("book{0}", index),
               };*/
            // Add(b);
            //}

        }



        public BookDto Get(string id)
        {
            var q = Query.EQ("_id", id);
            var book = _books.FindOne(q);
            book.Author = _database.GetCollection<Author>("Authors").FindOne(Query.EQ("_id", book.AuthorId));

            var dto = AutoMapper.Mapper.Map<BookDto>(book);
            return dto;
        }

        public IEnumerable<BookDto> GetAll()
        {
            MongoCursor<Book> cursor = _books.FindAll();
            var dto = AutoMapper.Mapper.Map<List<BookDto>>(cursor.ToList());
            return dto;
        }

        public void Remove(string id)
        {
            var query = Query.EQ("_id", id);
            _books.Remove(query);
        }

        public void Add(string name, string authorId)
        {
            var authors = _database.GetCollection<Author>("Authors");
            var author = authors.FindOne(Query.EQ("_id", authorId));

            var item = new Book { Name = name, Author = author };
            item.Id = ObjectId.GenerateNewId().ToString();
            _books.Insert(item);
            
        }

        public bool Update(string id, string name, string authorId)
        {
            var item = _books.FindOne(Query.EQ("_id", id));
            if (item == null) return false;

            item.Author = _database.GetCollection<Author>("Authors").FindOne(Query.EQ("_id", authorId));
            item.Name = name;

            _books.Save(item);

            return true;
        }
    }
}
