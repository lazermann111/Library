using Library3.DTO;
using Library3.Models;
using Library3.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Library3.Controllers
{
    public class BooksController : ApiController
    {

        private static MongoBookRepository mongo = new MongoBookRepository();
        private static PostgresBookRepository postgres = new PostgresBookRepository();

        private IBookrepository _repository;
        public BooksController()
        {
            _repository = WebApiApplication.MongoDbUsed ? (IBookrepository) mongo : postgres;
        }

            public IEnumerable<BookDto> GetAllBooks()
            {
                return _repository.GetAll();
            }

            public BookDto GetBook(string id)
            {
                BookDto item = _repository.Get(id);
                if (item == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                return item;
            }

            public IEnumerable<BookDto> GetBookByAuthor(string author)
            {
                return _repository.GetAll().Where(
                    p => p != null && string.Equals(p.Author.Name, author, StringComparison.OrdinalIgnoreCase));
            }

            public HttpResponseMessage PostBook(string name, string authorId)
            {
                _repository.Add( name, authorId);
                var response = Request.CreateResponse(HttpStatusCode.Created);

               
                return response;
            }

            public void PutBook(string id, string name, string authorId)
            {
                if (!_repository.Update(id, name, authorId))
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
            }

            public void DeleteBook(string id)
            {
               _repository.Remove(id);
            }
        
    }
}
