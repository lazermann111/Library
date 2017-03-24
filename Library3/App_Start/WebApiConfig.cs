using Library3.Models.Repositories;
using Library3.Repositories;
using Library3.Repositories.Async;
using Library3.Repositories.Sync;
using Library3.Resolver;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Library3
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {


            var container = new UnityContainer();
            container.RegisterType<IBookrepository, MongoBookRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IBookrepository, PostgresBookRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<BookRepositoryProxy, BookRepositoryProxy>(
                new InjectionConstructor(  
                    new ResolvedParameter<MongoBookRepository>(),
                    new ResolvedParameter<PostgresBookRepository>()
                ));

            container.RegisterType<IAuthorRepository, MongoAuthorRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IAuthorRepository, PostgresAuthorRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<AuthorRepositoryProxy, AuthorRepositoryProxy>(
               new InjectionConstructor(
                   new ResolvedParameter<MongoAuthorRepository>(),
                   new ResolvedParameter<PostgresAuthorRepository>()
               ));

            config.DependencyResolver = new UnityResolver(container);

            config.MapHttpAttributeRoutes();

          
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
