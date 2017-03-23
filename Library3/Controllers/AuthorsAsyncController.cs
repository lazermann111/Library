using Library3.DTO;
using Library3.Models;
using Library3.Models.Repositories;
using Library3.Models.Repositories.Async;
using Library3.Repositories;
using Library3.Repositories.Async;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Library3.Controllers
{
    [RoutePrefix("api/authorsAsync")]
    public class AuthorsAsyncController : ApiController
    {
        private static MongoAuthorRepositoryAsync mongo = new MongoAuthorRepositoryAsync();
        private static PostgresAuthorRepositoryAsync postgres = new PostgresAuthorRepositoryAsync();

        private IAuthorRepositoryAsync _repository;

        public AuthorsAsyncController()
        {
            _repository = WebApiApplication.MongoDbUsed ? (IAuthorRepositoryAsync) mongo: mongo;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<AuthorDto>> GetAllAuthors(int page)
        {
            return await _repository.GetAll(page);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<AuthorDto> GetAuthor(string id)
        {
            AuthorDto item = await _repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        [HttpPost]
        [Route("Save")]
        public async Task<HttpResponseMessage> PostAuthor(string name)
        {
            await _repository.Add(name);
            var response = Request.CreateResponse(HttpStatusCode.Created);
            return response;
        }

        [HttpPut]
        [Route("Update")]
        public async void PutAuthor(string id, string name)
        {       
            if (!( await _repository.Update(id, name)))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<HttpResponseMessage> DeleteAuthor(string id)
        {
            await _repository.Remove(id);
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

    }
}

