namespace NHibernateFluentExample.Entities
{
    public class Employee
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Department { get; set; }
        public virtual decimal Salary { get; set; }
    }
}
