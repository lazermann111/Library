using Library3.Models;
using Library3.Models.Repositories;
using Library3.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Library3.Controllers
{
    public class AuthorsController : ApiController
    {
        private static MongoAuthorRepository mongo = new MongoAuthorRepository();
        private static PostgresAuthorRepository postgres = new PostgresAuthorRepository();

        private IAuthorReposiory _repository;

        public AuthorsController()
        {
            _repository = WebApiApplication.MongoDbUsed ? (IAuthorReposiory) mongo: postgres;
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            return _repository.GetAll();
        }

        public Author GetAuthor(string id)
        {
            Author item = _repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

       
        public HttpResponseMessage PostAuthor(string name, string authorId)
        {
            var item = _repository.Add(name);
            var response = Request.CreateResponse<Author>(HttpStatusCode.Created, item);

            string uri = Url.Link("DefaultApi", new { id = item.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void PutAuthor(string id, string name)
        {       
            if (!_repository.Update(id, name))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void DeleteAuthor(string id)
        {
            _repository.Remove(id);
        }

    }
}

