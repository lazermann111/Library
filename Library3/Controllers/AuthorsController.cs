using Library3.DTO;
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
    [RoutePrefix("api/authors")]
    public class AuthorsController : ApiController
    {
        //private static MongoAuthorRepository mongo = new MongoAuthorRepository();
        //private static PostgresAuthorRepository postgres = new PostgresAuthorRepository();

        public IAuthorRepository _repository;

        public AuthorsController(IAuthorRepository repository)
        {
            //_repository = WebApiApplication.MongoDbUsed ? (IAuthorRepository) mongo: postgres;
            _repository = repository;
        }


        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<AuthorDto> GetAllAuthors(int page)
        {
            return _repository.GetAll(page);
        }

        [HttpGet]
        [Route("Get")]
        public AuthorDto GetAuthor(string id)
        {
            AuthorDto item = _repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        [HttpPost]
        [Route("Save")]
        public HttpResponseMessage PostAuthor(string name)
        {
             _repository.Add(name);
            var response = Request.CreateResponse(HttpStatusCode.Created);
            return response;
        }

        [HttpPut]
        [Route("Update")]
        public void PutAuthor(string id, string name)
        {       
            if (!_repository.Update(id, name))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
        [HttpDelete]
        [Route("Delete")]
        public void DeleteAuthor(string id)
        {
            _repository.Remove(id);
        }

    }
}

