using Library3.DTO;
using Library3.Helpers;
using Library3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library3.Repositories
{
    public class PostgresBookRepository : IBookrepository
    {
        public void Add(string name, string authorId)
        {
            var session = PostgresSessionManager.OpenSession();
            var author = new Book { Name = name};

            session.Save(author);
           
        }

        public BookDto Get(string id)
        {
            var book = PostgresSessionManager.OpenSession().Get<Book>(id);
            var dto = AutoMapper.Mapper.Map<BookDto>(book);
            return dto;
        }

        public IEnumerable<BookDto> GetAll()
        {
            var books = PostgresSessionManager.OpenSession().QueryOver<Book>().List();
            var dtos = AutoMapper.Mapper.Map<List<BookDto>>(books);
            return dtos;
        }

        public void Remove(string id)
        {
            var session = PostgresSessionManager.OpenSession();
            var book = session.Get<Book>(id);
            session.Delete(book);
        }

        public bool Update(string id, string name, string authorId)
        {
            var session = PostgresSessionManager.OpenSession();
            var book = session.Get<Book>(authorId);

            if (book == null) return false;

            book.Name = name;
            session.Update(book);
            return true;
        }
    }
}