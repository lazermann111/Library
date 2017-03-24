using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library3.Repositories.Sync
{
    public class AuthorRepositoryProxy
    {
        
            private readonly IAuthorRepository mongo;
            private readonly IAuthorRepository postgres;


            public AuthorRepositoryProxy(
                IAuthorRepository mongo, IAuthorRepository postgres)
            {
                this.mongo = mongo;
                this.postgres = postgres;
            }

            public IAuthorRepository Repository => WebApiApplication.MongoDbUsed ? mongo : postgres;

    }
}