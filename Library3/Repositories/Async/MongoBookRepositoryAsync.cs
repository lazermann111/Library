
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
using System.Threading.Tasks;

namespace Library3.Repositories.Async
{
    public class MongoBookRepositoryAsync : IBookrepositoryAsync
    {
      
        IMongoCollection<MongoBook> _books;

        public MongoBookRepositoryAsync()
        {
            _books = MongoSessionManager.Database.GetCollection<MongoBook>("Books");
        }

        public async Task<BookDto> Get(string id)
        {
            var q = Query.EQ("_id", id);
            var book = await _books.FindAsync(b => b.Id == id);
            var res = book.FirstOrDefault()?.Map();
            return res;                 
        }

        public async Task<IEnumerable<BookDto>> GetAll()
        {
            var  cursor = await _books.Find(_ => true).ToListAsync();
            var c =  cursor.ToList().Select(book => book.Map());
            
            return c;
        }

        public async Task Remove(string id)
        {
           //var query = Query.EQ("_id", id);
          var res = await  _books.DeleteOneAsync(a => a.Id ==id);
        }

        public async Task Add(string name, string authorId)
        {
            var authors = MongoSessionManager.Database.GetCollection<MongoAuthor>("Authors");
            var author = await authors.FindAsync( a => a.Id == authorId);

            var item = new MongoBook
            {
                Name = name,
                AuthorId =
                    new MongoDBRef("Authors", author.FirstOrDefault()?.Id),
                Id = ObjectId.GenerateNewId().ToString()
            };
            await _books.InsertOneAsync(item);
            
        }

        public async Task<bool> Update(string id, string name, string authorId)
        {
            var task = await _books.FindAsync(a => a.Id == authorId);
            var item = task.FirstOrDefault();
            if (item == null) return false; 
            item.Name = name;

           await _books.InsertOneAsync(item);

            return true;
        }
    }
}
