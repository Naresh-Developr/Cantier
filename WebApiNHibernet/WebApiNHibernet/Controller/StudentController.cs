using FluentNHibernate.Conventions;
using Microsoft.AspNetCore.Mvc;
using WebApiNHibernate.Models;
using WebApiNHibernate.Repositories;

namespace WebApiNhibernate.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentRepository _repository;

        public StudentController()
        {
            _repository = new StudentRepository();
        }

        [HttpGet]
        [Route("getAll")]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            if(_repository.GetAll().IsEmpty())
            {
                NoContent();    
            }
           
            return Ok(_repository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Student> GetStudentById(int id)
        {

            var student = _repository.GetByid(id);
            if(student == null)
            {
                NoContent();
            }
            if (student == null) return NotFound();
            return Ok(student);
        }

        [HttpPost]
        public ActionResult<Student> CreateStudent([FromBody] Student student)
        {
            if (student == null) return BadRequest();
            _repository.Add(student);
            return Ok(student);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, [FromBody] Student student)
        {
            var existingStudent = _repository.GetByid(id);
            if (existingStudent == null) return NotFound();

            existingStudent.Name = student.Name;
            existingStudent.Email = student.Email;
            existingStudent.Date = student.Date;

            _repository.Update(existingStudent);
            return NoContent();
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteStudent(int id)
        {
            _repository.Delete(id);
            return NoContent();
        }

    }
}
