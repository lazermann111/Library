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


            Mapper.CreateMap<MongoBook, BookDto>();
                /*.MaxDepth(2)
                .ForMember(entity => entity.Author, expression => expression.ResolveUsing(a =>
                {
                    if (a?.AuthorId == null) return null;
                    var table = MongoSessionManager.Database.GetCollection<MongoAuthor>("Authors");
                    return Mapper.Map<AuthorDto>(table.FindOneById(a.AuthorId)) ;
                })).MaxDepth(2);*/

            Mapper.CreateMap<MongoAuthor, AuthorDto>();
            /*.MaxDepth(2)
              .ForMember(entity => entity.Books, expression => expression.ResolveUsing(a =>
                {
                    if (a?.BookIds == null) return null;
                    var table = MongoSessionManager.Database.GetCollection<MongoBook>("Books");
                  
                    var q = Query.In("_id",  a.BookIds);
                    return Mapper.Map<List<BookDto>>(table.Find(q).ToList());
                })).MaxDepth(2)
                .ReverseMap()
             .ForMember(entity => entity.BookIds, expression => expression.MapFrom(a => a.Books.Select(b => b.Id)));
             */

            /*ResolveUsing(a =>
                {
                    var table = MongoSessionManager.Database.GetCollection<MongoBook>("Books");
                    return Mapper.Map<List<BookDto>>( table.FindAllAs<MongoBook>().ToList());
                })); */
        }
    }
}