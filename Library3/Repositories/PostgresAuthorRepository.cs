using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Library3.Models;
using Library3.Repositories;

namespace Library3.Models.Repositories
{
    public class PostgresAuthorRepository : IAuthorReposiory
    {
        public Author Add(string name)
        {
            throw new NotImplementedException();
        }

        public Author Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Author> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(string id)
        {
            throw new NotImplementedException();
        }

        public bool Update(string authorId, string name)
        {
            throw new NotImplementedException();
        }
    }
}