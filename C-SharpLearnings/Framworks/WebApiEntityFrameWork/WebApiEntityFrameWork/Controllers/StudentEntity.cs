using Microsoft.EntityFrameworkCore;
using WebApiEntityFrameWork.Data;

namespace WebApiEntityFrameWork.Controllers
{
    public class StudentEntity : DbContext
    {
        public DbSet<Student> Student { get; set; }

        public StudentEntity(DbContextOptions<StudentEntity> options) : base(options)
        {


        }

        // Fetch all students using stored procedure
        //public List<Student> GetStudentsFromStoredProc()
        //{
        //    return Student.FromSqlRaw("EXEC GetAllStudents").ToList();
        //}


    }
}
