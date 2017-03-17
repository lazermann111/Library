using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Library3.Models;
using Library3.Repositories;
using Library3.Helpers;
using Library3.DTO;

namespace Library3.Models.Repositories
{
    public class PostgresAuthorRepository : IAuthorReposiory
    {
        public void Add(string name)
        {
            var session = PostgresSessionManager.OpenSession();
            var author = new Author { Name = name, Books = new List<Book>()};

            session.Save(author);
        }

        public AuthorDto Get(string id)
        {
            var author =  PostgresSessionManager.OpenSession().Get<Author>(id);
            var dto = AutoMapper.Mapper.Map<AuthorDto>(author);
            return dto;
        }

        public IEnumerable<AuthorDto> GetAll()
        {
            var authors = PostgresSessionManager.OpenSession().QueryOver<Author>().List();
            var dtos = AutoMapper.Mapper.Map<List<AuthorDto>>(authors);
            return dtos;
        }

        public void Remove(string id)
        {
            var session = PostgresSessionManager.OpenSession();
            var author = session.Get<Author>(id);
            session.Delete(author);
        }

        public bool Update(string authorId, string name)
        {
            var session = PostgresSessionManager.OpenSession();
            var author = session.Get<Author>(authorId);

            if (author == null) return false;

            author.Name = name;
            session.Update(author);
            return true;
        }
    }
}