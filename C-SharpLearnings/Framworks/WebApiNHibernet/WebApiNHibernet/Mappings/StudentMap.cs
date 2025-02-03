using FluentNHibernate.Mapping;
using WebApiNHibernate.Models;

namespace WebApiNHibernet.Mappings
{
    public class StudentMap : ClassMap<Student>
    {
        public StudentMap() 
        {
            Table("Students");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name).Not.Nullable();
            Map(x => x.Email).Not.Nullable();
            Map(x => x.Date).Not.Nullable();
        
        }

    }
}
