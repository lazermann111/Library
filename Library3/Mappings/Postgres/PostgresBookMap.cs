using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;


namespace Library3.Postgres
{
    public class PostgresBookMap : ClassMap<PostgresBook>
    {
        public PostgresBookMap()
        {
            Id(e => e.Id);
            Map(e => e.Name);

            References(a => a.Author);
        }
    }
}