using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;


namespace Library3.Postgres
{
    public class PostgresAuthorMap : ClassMap<PostgresAuthor>
    {
        public PostgresAuthorMap()
        {
            Id(e => e.Id);
            Map(e => e.Name);

            HasMany(a => a.Books).Cascade.AllDeleteOrphan();
        }
    }
}