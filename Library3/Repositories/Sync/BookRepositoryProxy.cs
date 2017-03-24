using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library3.Repositories.Async
{
    public class BookRepositoryProxy
    {
        
            private readonly IBookrepository mongo;
            private readonly IBookrepository postgres;

       
            public BookRepositoryProxy(
                IBookrepository mongo, IBookrepository postgres)
            {
                this.mongo = mongo;
                this.postgres = postgres;
            }

        public IBookrepository Repository => WebApiApplication.MongoDbUsed ? mongo : postgres;

    }
}