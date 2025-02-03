using Microsoft.AspNetCore.Mvc;
using FirstwebApp.Models;

namespace FirstwebApp.Controllers
{
    [Route("/api")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        
        public StudentController(ILogger<StudentController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            _logger.LogInformation("get all students called");
            var students = new List<Student>();

            foreach(var item in CollegeRepository.Students)
            {
                Student obj = new Student()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Email = item.Email,
                    Address = item.Address


                };
                students.Add(obj);
            }
            return Ok(students);
        }

        [HttpPost]
        [Route("post", Name ="Create student")]

        public ActionResult<Student> CreateStudent([FromBody]Student model)
        {
            if(model== null)
            {
                return BadRequest();
            }
            var newId = CollegeRepository.Students.LastOrDefault().Id+1;
            Student st = new Student
            {
                Id = newId,
                Name = model.Name,
                Email = model.Email,
                Address = model.Address

            };
            CollegeRepository.Students.Add(st);
            model.Id = st.Id;

            return Ok(model);
        }

        [HttpPut]
        [Route("update", Name = "update a record")]

        public ActionResult UpdateDetails([FromBody] Student model)
        {
            if(model==null || model.Id <= 0) 
                BadRequest();

            var existingRecord = CollegeRepository.Students.Where(n => n.Id == model.Id).FirstOrDefault();

            existingRecord.Name = model.Name; 
            existingRecord.Email = model.Email;
            existingRecord.Address = model.Address;

            return NoContent();
        }






        [HttpGet]
        [Route("get/{id}", Name = "get all students")]
        public ActionResult<Student > GetStudentsById(int id)
        {
            if (id <= 0)
            {
                //badRequest--400
                return  BadRequest();
                
            }
            return CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
        }

        [HttpGet]
        [Route("delete/{id}", Name = "delete by id of students")]
        public bool DeleteStudentsById(int id)
        {
            var stu = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
            CollegeRepository.Students.Remove(stu);

            return true;

        }

        [HttpGet]
        [Route("{name}", Name = "get by name students")]
        public Student GetStudentByName(string name)
        {
            return CollegeRepository.Students.Where(n => n.Name == name).FirstOrDefault();
        }
    }
}
