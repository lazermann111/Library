using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Library3.DTO;
using Library3.Entities.Mongo;
using MongoDB.Driver;

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
                Author = MongoSessionManager.Database.FetchDBRefAs<MongoAuthor>(b?.AuthorId).BaseMap()
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
                Books = a.BookIds
                .Select(book => MongoSessionManager.Database.FetchDBRefAs<MongoBook>(book)
                .BaseMap()).ToList()
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
    }
}