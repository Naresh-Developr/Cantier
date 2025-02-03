using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NHibernateFluentExample.Mappings;

namespace NHibernateFluentExample.Helper
{
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        public static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    _sessionFactory = Fluently.Configure()
                        .Database(MsSqlConfiguration.MsSql2012
                            .ConnectionString(@"Server=localhost;Database=EmployeeDb;Trusted_Connection=True;TrustServerCertificate=True;")
                            .Driver<NHibernate.Driver.MicrosoftDataSqlClientDriver>()) // Use Microsoft.Data.SqlClient explicitly
                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<EmployeeMap>())
                        .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(false, false))
                        .BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
