using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Library3.DTO;
using Library3.Entities.Mongo;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Library3.Helpers
{
    public static class Mappers
    {
        public static BookDto Map(this MongoBook b)
        {
            return new BookDto()
            {
                Id =  b.Id,
                Name = b.Name,
              //  Author = MongoSessionManager.Database.FetchDBRef<MongoAuthor>(b?.AuthorId)?.BaseMap()
            };
        }

        public static BookDto BaseMap(this MongoBook b)
        {
            return new BookDto()
            {
                Id = b.Id,
                Name = b.Name,  
            };
        }

        public static AuthorDto Map(this MongoAuthor a)
        {
            return new AuthorDto()
            {
                Id = a.Id,
                Name = a.Name,
             //   Books = a.BookIds
             //   .Select(book =>  MongoSessionManager.Database.FetchDBRef<MongoBook>(book)?.BaseMap()).ToList()
            };
        }

        public static AuthorDto BaseMap(this MongoAuthor a)
        {
            return new AuthorDto()
            {
                Id = a.Id,
                Name = a.Name,
            };
        }

        public static  T FetchDBRef<T>(this IMongoDatabase database, MongoDBRef reference) where T : MongoEntity
        {
            var filter = Builders<T>.Filter.Eq(e => e.Id, reference.Id.AsString);
            return  database.GetCollection<T>(reference.CollectionName).Find(filter).FirstOrDefault();
        }
    }
}