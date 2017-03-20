using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Library3.DTO;
using Library3.Entities.Mongo;

using Library3.Models;
using Library3.Postgres;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Library3.Helpers
{
    public class AutomapperConfiguration
    {

        internal static void Configure()
        {
            Mapper.CreateMap<PostgresBook, BookDto>();
            Mapper.CreateMap<PostgresAuthor, AuthorDto>();

          /*  Mapper.CreateMap<MongoBook, BookBaseDto>();
            Mapper.CreateMap<MongoAuthor, AuthorBaseDto>();
            Mapper.CreateMap<MongoBook, BookDto>()
                .MaxDepth(1)
                .ForMember(entity => entity.Author, expression => expression.ResolveUsing(a =>
                {
                    Console.WriteLine("AuthorBaseDto !!");
                    return a?.AuthorId == null ? null : Mapper.Map<AuthorBaseDto>(MongoSessionManager.Database.FetchDBRefAs<MongoAuthor>(a?.AuthorId));
                    // var table = MongoSessionManager.Database.GetCollection<MongoAuthor>("Authors");
                }));

            Mapper.CreateMap<MongoAuthor, AuthorDto>()
            .MaxDepth(1)
              .ForMember(entity => entity.Books, expression => expression.ResolveUsing(a =>
                {
                    // var table = MongoSessionManager.Database.GetCollection<MongoBook>("Books");

                    return a?.BookIds?.Select(b => Mapper.Map<BookBaseDto>(MongoSessionManager.Database.FetchDBRefAs<MongoBook>(b))).ToList();
                }))
                .ReverseMap();*/  
        }
    }
}