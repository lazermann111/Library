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

            public IEnumerable<Book> GetAllBooks()
            {
                return _repository.GetAll();
            }

            public Book GetBook(string id)
            {
                Book item = _repository.Get(id);
                if (item == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                return item;
            }

            public IEnumerable<Book> GetBookByAuthor(string author)
            {
                return _repository.GetAll().Where(
                    p => string.Equals(p.Author.Name, author, StringComparison.OrdinalIgnoreCase));
            }

            public HttpResponseMessage PostBook(string name, string authorId)
            {
                var item = _repository.Add( name, authorId);
                var response = Request.CreateResponse<Book>(HttpStatusCode.Created, item);

                string uri = Url.Link("DefaultApi", new { id = item.Id });
                response.Headers.Location = new Uri(uri);
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
