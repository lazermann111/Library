using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Logging;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Conventions.Helpers;
using Library3.Postgres;

namespace Library3.Helpers
{
    public static class PostgresSessionManager
    {
        private static readonly ISessionFactory sessionFactory;


        static PostgresSessionManager()
        {
            AutoPersistenceModel model = AutoMap.Assembly(System.Reflection.Assembly.GetCallingAssembly())
            .Where(t => t.Namespace == "Library3.Entities.Postgres");

            sessionFactory = Fluently.Configure()
                .Database(PostgreSQLConfiguration.Standard
                .ConnectionString(c => c
                    .Host("localhost")
                    .Port(5432)
                    .Database("test")
                    .Username("postgres")
                    .Password("postgres")))
                 .Mappings(c => c.FluentMappings.AddFromAssemblyOf<PostgresAuthorMap>().Conventions.Add(AutoImport.Never()))
                .ExposeConfiguration(config => new SchemaExport(config).Create(false, true))
                .BuildSessionFactory();
        }


        public static ISessionFactory SessionFactory
        {
            get { return sessionFactory; }
        }


        public static ISession OpenSession()
        {
            return sessionFactory.OpenSession();
        }
    }
}