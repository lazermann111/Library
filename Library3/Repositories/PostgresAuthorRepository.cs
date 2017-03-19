using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Library3.Models;
using Library3.Repositories;
using Library3.Helpers;
using Library3.DTO;
using Library3.Postgres;


namespace Library3.Models.Repositories
{
    public class PostgresAuthorRepository : IAuthorReposiory
    {
        public void Add(string name)
        {
            var session = PostgresSessionManager.OpenSession();
            var author = new PostgresAuthor() { Name = name, Books = new List<PostgresBook>()};

            session.Save(author);
        }

        public AuthorDto Get(string id)
        {
            var author =  PostgresSessionManager.OpenSession().Get<PostgresAuthor>(Int16.Parse(id));
            var dto = AutoMapper.Mapper.Map<AuthorDto>(author);
            return dto;
        }

        public IEnumerable<AuthorDto> GetAll(int page)
        {
            var authors = PostgresSessionManager.OpenSession().QueryOver<PostgresAuthor>().List().OrderBy(a => a.Name).Skip(page * 10).Take(10);
            var dtos = AutoMapper.Mapper.Map<List<AuthorDto>>(authors);
            return dtos;
        }

        public void Remove(string id)
        {
            var session = PostgresSessionManager.OpenSession();
            var author = session.Get<PostgresAuthor>(Int16.Parse(id));
            session.Delete(author);
        }

        public bool Update(string authorId, string name)
        {
            var session = PostgresSessionManager.OpenSession();
            var author = session.Get<PostgresAuthor>(Int16.Parse(authorId));

            if (author == null) return false;

            author.Name = name;
            session.Update(author);
            return true;
        }
    }
}