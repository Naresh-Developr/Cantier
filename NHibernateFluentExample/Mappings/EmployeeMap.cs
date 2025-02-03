using FluentNHibernate.Mapping;
using NHibernateFluentExample.Entities;

namespace NHibernateFluentExample.Mappings
{
    public class EmployeeMap : ClassMap<Employee>
    {
        public EmployeeMap()
        {
            Table("Employees");  // Name of the table in DB
            Id(x => x.Id).GeneratedBy.Identity();  // Auto-increment ID
            Map(x => x.Name).Length(100).Not.Nullable();
            Map(x => x.Department).Length(50);
            Map(x => x.Salary).Not.Nullable();
        }
    }
}
