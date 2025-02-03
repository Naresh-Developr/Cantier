using Microsoft.AspNetCore.Identity;
using WebApiNHibernate.Models;
using NHibernateFluentExample.Helper;
using Microsoft.AspNetCore.Http.HttpResults;



namespace WebApiNHibernate.Repositories
{
    public class StudentRepository
    {

       public void Add(Student student)
       {
            using (var session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session.Save(student);
                transaction.Commit();
            }
       }


        public List<Student> GetAll()
        {
            using (var sessions = NHibernateHelper.OpenSession())
            {
                var ans = sessions.Query<Student>().ToList();
                if(ans == null)
                {
                    return new List<Student>();
                }
                return ans;
            }
        }

        public Student GetByid(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                if (id <= 0)
                {
                    return null;
                }
                var ans = session.Query<Student>().FirstOrDefault();

                return ans;
            }
        }

        public void Update(Student student)
        {
            using (var sessions = NHibernateHelper.OpenSession())
            using (var transaction = sessions.BeginTransaction())
            {
                sessions.Update(student);
                transaction.Commit();
            }
        }

        public void Delete(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session.Delete(id);
                transaction.Commit();
            }
        
        }

    }
}
