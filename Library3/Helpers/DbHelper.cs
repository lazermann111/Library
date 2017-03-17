using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Library3.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library3.Helpers
{
    public class DbHelper
    {
        public static void GenerateMongoDbContent()
        {

            var server = new MongoClient("mongodb://localhost:27017").GetServer();
            var _books = server.GetDatabase("local").GetCollection<Book>("BookIds");
            var _authors = server.GetDatabase("local").GetCollection<Book>("Authors");

            _books.RemoveAll();
            _authors.RemoveAll();

            IList<Book> books = new List<Book>();
            IList<Author> authors = new List<Author>();

            for (int index = 1; index < 3; index++)
            {
                Book book = new Book
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Name = string.Format("book{0}", index),
                };
                books.Add(book);
            }
            
            for (int index = 1; index < 3; index++)
            {
                Author author = new Author
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Name = string.Format("author{0}", index),
                };
                authors.Add(author);
            }

            books[0].AuthorId = authors[0].Id;
            authors[0].BookIds = new List<string> { books[0].Id };

            foreach (var book in books) _books.Insert(book);
            foreach (var author in authors) _authors.Insert(author);
            
        }

        public static void GeneratePostgresContent()
        {
            IList<Book> books = new List<Book>();
            IList<Author> authors = new List<Author>();

            for (int index = 1; index < 3; index++)
            {
                Book book = new Book
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Name = string.Format("book{0}", index),
                };
                books.Add(book);
            }

            for (int index = 1; index < 3; index++)
            {
                Author author = new Author
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Name = string.Format("author{0}", index),
                };
                authors.Add(author);
            }

            

            using (var session = PostgresSessionManager.OpenSession())
            {
                var tx = session.BeginTransaction();
                foreach (var book in books) session.Save(book);
                foreach (var author in authors) session.Save(author);

                books[0].Author = authors[0];
                authors[0].Books = new List<Book> { books[0] };

                session.Update(books[0]);
                session.Update(authors[0]);

                tx.Commit();
            }
        }
       
    }
}