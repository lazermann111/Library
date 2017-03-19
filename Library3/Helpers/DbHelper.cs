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
        public static void GenerateMongoDbContent()
        {

            var server = new MongoClient("mongodb://localhost:27017").GetServer();
            var _books = server.GetDatabase("local").GetCollection<MongoBook>("Books");
            var _authors = server.GetDatabase("local").GetCollection<MongoBook>("Authors");

            _books.RemoveAll();
            _authors.RemoveAll();

            IList<MongoBook> books = new List<MongoBook>();
            IList<MongoAuthor> authors = new List<MongoAuthor>();

            for (int index = 1; index < 30; index++)
            {
                MongoBook book = new MongoBook
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Name = string.Format("book{0}", index),
                };
                books.Add(book);
            }
            
            for (int index = 1; index < 30; index++)
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
                books[i].AuthorId = new MongoDBRef(_authors.Name, authors[i].Id);
                authors[i].BookIds = new List<MongoDBRef> {new MongoDBRef(_books.Name, books[i].Id) };
            }
            foreach (var book in books) _books.Insert(book);
            foreach (var author in authors) _authors.Insert(author);
            
        }

        public static void GeneratePostgresContent()
        {
            IList<PostgresBook> books = new List<PostgresBook>();
            IList<PostgresAuthor> authors = new List<PostgresAuthor>();

            for (int index = 1; index < 30; index++)
            {
                PostgresBook book = new PostgresBook
                {
                   // Id = ObjectId.GenerateNewId().ToString(),
                    Name = string.Format("book{0}", index),
                };
                books.Add(book);
            }

            for (int index = 1; index < 30; index++)
            {
                PostgresAuthor author = new PostgresAuthor
                {
                //    Id = ObjectId.GenerateNewId().ToString(),
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