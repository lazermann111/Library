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
using AutoMapper;
using Library3.Entities.Mongo;
using Library3.Postgres;


namespace Library3.Helpers
{
    public class DbHelper
    {
        private const int dbEnttries = 50000;

        public static void GenerateMongoDbContent()
        {

            
            var _books =   MongoSessionManager.Database.GetCollection<MongoBook>("Books");
            var _authors = MongoSessionManager.Database.GetCollection<MongoAuthor>("Authors");

           

            IList<MongoBook> books = new List<MongoBook>();
            IList<MongoAuthor> authors = new List<MongoAuthor>();

            for (int index = 1; index < dbEnttries; index++)
            {
                MongoBook book = new MongoBook
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Name = string.Format("book{0}", index),
                };
                books.Add(book);
            }
            
            for (int index = 1; index < dbEnttries; index++)
            {
                MongoAuthor author = new MongoAuthor
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Name = string.Format("author{0}", index),
                };
                authors.Add(author);
            }

            for (var i = 0; i < books.Count; i++)
            {
                books[i].AuthorId = new MongoDBRef("Authors", authors[i].Id);
                authors[i].BookIds = new List<MongoDBRef> {new MongoDBRef("Books", books[i].Id) };
            }
            _books.InsertMany(books);
            _authors.InsertMany(authors);
            
        }

        public static void GeneratePostgresContent()
        {
            IList<PostgresBook> books = new List<PostgresBook>();
            IList<PostgresAuthor> authors = new List<PostgresAuthor>();

            for (int index = 1; index < dbEnttries; index++)
            {
                PostgresBook book = new PostgresBook
                {
                   // Id = ObjectId.GenerateNewId().ToString(),
                    Name = string.Format("book{0}", index),
                };
                books.Add(book);
            }

            for (int index = 1; index < dbEnttries; index++)
            {
                PostgresAuthor author = new PostgresAuthor
                {
             
                    Name = string.Format("author{0}", index),
                };
                authors.Add(author);
            }

            

            using (var session = PostgresSessionManager.OpenSession())
            {
                var tx = session.BeginTransaction();
                foreach (var book in books) session.Save(book);
                foreach (var author in authors) session.Save(author);

                for (var i = 0; i < books.Count; i++)
                {
                    books[i].Author = authors[i];
                    authors[i].Books = new List<PostgresBook> { books[i] };
                }


                foreach (var book in books) session.Save(book);
                foreach (var author in authors) session.Save(author);

                tx.Commit();
            }
        }
    }
}