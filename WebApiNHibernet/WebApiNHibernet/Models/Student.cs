namespace WebApiNHibernate.Models
{
    public class Student
    {
        public virtual int Id { get; set; } 
        public virtual string Name { get; set; }    

        public virtual string Email {get; set; }

        public virtual DateTime Date { get; set; }
    }
}
