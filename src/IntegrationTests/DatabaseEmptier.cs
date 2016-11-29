using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using NHibernate;
using NHibernate.Transform;

namespace ClearMeasure.Bootcamp.IntegrationTests
{
    public class DatabaseEmptier
    {
        private readonly ISessionFactory _factory;
        //should not clear out two tables from test run to test run.
        private static readonly string[] _ignoredTables = new[] { "[dbo].[sysdiagrams]", "[dbo].[usd_AppliedDatabaseScript]" };
        private static string _deleteSql;

        public DatabaseEmptier(ISessionFactory factory)
        {
            _factory = factory;
        }

        private class Relationship
        {
            public string PrimaryKeyTable { get; private set; }
            public string ForeignKeyTable { get; private set; }
        }

        public virtual void DeleteAllData()
        {
            if(_deleteSql == null)
            {
                _deleteSql = BuildDeleteTableSqlStatement();
            }

            ISession session = _factory.OpenSession();

            using (IDbCommand command = session.Connection.CreateCommand())
            {
                command.CommandText = _deleteSql;
                command.ExecuteNonQuery();
            }
        }

        private string BuildDeleteTableSqlStatement()
        {
            ISession session = _factory.OpenSession();

            IList<string> allTables = GetAllTables(session);
            IList<Relationship> allRelationships = GetRelationships(session);
            string[] tablesToDelete = BuildTableList(allTables, allRelationships);

            return BuildTableSql(tablesToDelete);
        }

        private static string BuildTableSql(IEnumerable<string> tablesToDelete)
        {
            string completeQuery = "";
            foreach (string tableName in tablesToDelete)
            {
                completeQuery += String.Format("delete from {0};", tableName);
            }
            return completeQuery;
        }

        private static string[] BuildTableList(ICollection<string> allTables, ICollection<Relationship> allRelationships)
        {
            var tablesToDelete = new List<string>();

            while (allTables.Any())
            {
                string[] leafTables = allTables.Except(allRelationships.Select(rel => rel.PrimaryKeyTable)).ToArray();

                if(leafTables.Length == 0)
                {
                    tablesToDelete.AddRange(allTables);
                    break;
                }

                tablesToDelete.AddRange(leafTables);

                foreach (string leafTable in leafTables)
                {
                    allTables.Remove(leafTable);
                    Relationship[] relToRemove =
                        allRelationships.Where(rel => rel.ForeignKeyTable == leafTable).ToArray();
                    foreach (Relationship rel in relToRemove)
                    {
                        allRelationships.Remove(rel);
                    }
                }
            }

            return tablesToDelete.ToArray();
        }

        private static IList<Relationship> GetRelationships(ISession session)
        {
            ISQLQuery otherquery =
                session.CreateSQLQuery(
                    @"select
	'[' + ss_pk.name + '].[' + so_pk.name + ']' as PrimaryKeyTable
, '[' + ss_fk.name + '].[' + so_fk.name + ']' as ForeignKeyTable
from
	sysforeignkeys sfk
	  inner join sysobjects so_pk on sfk.rkeyid = so_pk.id
	  inner join sys.tables st_pk on so_pk.id = st_pk.object_id
	  inner join sys.schemas ss_pk on st_pk.schema_id = ss_pk.schema_id
	  inner join sysobjects so_fk on sfk.fkeyid = so_fk.id
	  inner join sys.tables st_fk on so_fk.id = st_fk.object_id
	  inner join sys.schemas ss_fk on st_fk.schema_id = ss_fk.schema_id
order by
	so_pk.name
,   so_fk.name;");

            return otherquery.SetResultTransformer(Transformers.AliasToBean<Relationship>()).List<Relationship>();
        }

        private static IList<string> GetAllTables(ISession session)
        {
            ISQLQuery query =
                session.CreateSQLQuery(
                    @"select '[' + s.name + '].[' + t.name + ']'
from sys.tables t
inner join sys.schemas s on t.schema_id = s.schema_id");

            return query.List<string>().Except(_ignoredTables).ToList();
        }
    }
}