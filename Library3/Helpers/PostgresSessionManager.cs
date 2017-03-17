using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library3.Helpers
{
    public static class PostgresSessionManager
    {
        private static readonly ISessionFactory sessionFactory;


        static PostgresSessionManager()
        {
            AutoPersistenceModel model = AutoMap.Assembly(System.Reflection.Assembly.GetCallingAssembly())
            .Where(t => t.Namespace == "Library3.Models");

            sessionFactory = Fluently.Configure()
                .Database(PostgreSQLConfiguration.Standard
                .ConnectionString(c => c
                    .Host("localhost")
                    .Port(5432)
                    .Database("test")
                    .Username("postgres")
                    .Password("postgres")))
                .Mappings(m => m
                    .AutoMappings.Add(model))
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