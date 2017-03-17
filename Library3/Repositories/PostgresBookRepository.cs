using Library3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library3.Repositories
{
    public class PostgresBookRepository : IBookrepository
    {
        public Book Add(string name, string authorId)
        {
            throw new NotImplementedException();
        }

        public Book Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(string id)
        {
            throw new NotImplementedException();
        }

        public bool Update(string id, string name, string authorId)
        {
            throw new NotImplementedException();
        }
    }
}