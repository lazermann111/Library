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

namespace Library3.Repositories
{
    public class MongoAuthorRepository : IAuthorReposiory
    {

        MongoClient _client;
        MongoDatabase _database;
        MongoCollection<Author> _authors;

        public MongoAuthorRepository()
        {


            _client = new MongoClient("mongodb://localhost:27017");
            var server = _client.GetServer();
            _database = server.GetDatabase("local");
            _authors = _database.GetCollection<Author>("Authors");


         /*   _authors.RemoveAll();
            for (int index = 1; index < 3; index++)
            {
                AuthorId b = new AuthorId
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Name = string.Format("author{0}", index),
                };
                Add(b);
            }*/

        }

       

        public IEnumerable<AuthorDto> GetAll()
        {
            MongoCursor<Author> cursor = _authors.FindAll();
            var dto = AutoMapper.Mapper.Map<List<AuthorDto>>(cursor.ToList());
            return dto;
        }

        public AuthorDto Get(string id)
        {
          // var filter = Builders<AuthorId>.Filter.Eq("Id", id);
          // var filter2 = Builders<AuthorId>.Filter.Eq("_id", id);
          // var filter3 = Builders<AuthorId>.Filter.Eq("Name", id);
           var q = Query.EQ("_id", id);
           var author = _authors.FindOne(q);
           var dto = AutoMapper.Mapper.Map<AuthorDto>(author);
           return dto;
        }

       

        public void Remove(string id)
        {
            var query = Query.EQ("_id", id);
            _authors.Remove(query);
        }

        public bool Update(string authorId, string name)
        {
            var item = _authors.FindOne(Query.EQ("_id", authorId));
            if (item == null) return false;

            item.Name = name;
            _authors.Save(item);

            return true;
        }

        public void Add(string name)
        {
            Author b = new Author
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = string.Format("author{0}", name),
            };
            _authors.Insert(b);
        }
    }
}