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
using System.Threading.Tasks;

namespace Library3.Repositories.Async
{
    public class MongoAuthorRepositoryAsync : IAuthorRepositoryAsync
    {
   
        IMongoCollection<MongoAuthor> _authors;

        public MongoAuthorRepositoryAsync()
        {
            _authors = MongoSessionManager.Database.GetCollection<MongoAuthor>("Authors");
        }


        public async Task<IEnumerable<AuthorDto>> GetAll(int page)
        {
            var cursor = await _authors.FindAsync(_ => true);
            var dto = cursor.ToEnumerable().OrderBy(a => a.Name).Skip(page * 10).Take(10).Select(d => d.Map());
            return dto;
        }


        async Task<AuthorDto> IAuthorRepositoryAsync.Get(string id)
        {
            //var q = Query.EQ("_id", id);

            var a = await _authors.FindAsync(b => b.Id == id);
            var res = a.FirstOrDefault()?.Map();

            return res;
        }

        async Task IAuthorRepositoryAsync.Add(string name)
        {
            MongoAuthor b = new MongoAuthor
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = $"author{name}",
            };
           await _authors.InsertOneAsync(b);
        }

        async Task IAuthorRepositoryAsync.Remove(string id)
        {
            //var query = Query.EQ("_id", id);
          await  _authors.DeleteOneAsync(a => a.Id == id);
        }

        async Task<bool> IAuthorRepositoryAsync.Update(string authorId, string name)
        {
            var task = await _authors.FindAsync(n => n.Id == authorId);
            var item = task.FirstOrDefault();
            if (item == null) return false;

            item.Name = name;
            await _authors.InsertOneAsync(item);

            return true;
        }
    }
}