using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApiEntityFrameWork.Data;
using WebApiEntityFrameWork.Models;
//using log4net;

namespace WebApiEntityFrameWork.Controllers
{
    [Route("/api")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;

        private readonly StudentEntity _dbContext;

        //private static readonly ILog _logger = LogManager.GetLogger(typeof(StudentController));


        

        public StudentController(ILogger<StudentController> logger , StudentEntity dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;

        }

        [HttpGet("test")]
        public IActionResult TestLog()
        {
            _logger.LogDebug("Debug message");
            
            _logger.LogWarning("Warning message");
         

            return Ok("Logs saved to database using EF Core!");
        }


        //[HttpGet]
        //public ActionResult<IEnumerable<StudentMod>> GetStudents()
        //{
        //    _logger.LogInformation("get all students called");
        //    var students = _dbContext.Student.Select(item => new StudentMod()
        //    {
        //        Id = item.Id,
        //        Name = item.Name,
        //        Email = item.Email,
        //        Date = item.Date
        //    }).ToList();

        //    return Ok(students);
        //}

        [HttpGet]
        public ActionResult<List<Student>> GetStudentsFromStoredProc()
        {
           
            return Ok(_dbContext.Student.FromSqlRaw("EXEC GetAllStudents").ToList());
        }

        [HttpPost]
        [Route("post", Name = "Create student")]

        public ActionResult<StudentMod> CreateStudent([FromBody] StudentMod model)
        {
            if (model == null)
            {
                return BadRequest();
            }
             
            Student st = new Student
            {
               
                Name = model.Name,
                Email = model.Email,
                Date = model.Date

            };
            _dbContext.Student.Add(st);
            _dbContext.SaveChanges();



            return Ok(st);
        }

        [HttpPut]
        [Route("update", Name = "update a record")]

        public ActionResult UpdateDetails([FromBody] StudentMod model)
        {
            if (model == null || model.Id <= 0)
                BadRequest();

            var existingRecord = _dbContext.Student.Where(n => n.Id == model.Id).FirstOrDefault();
            if (existingRecord == null)
            {
                return NotFound();
            }

            existingRecord.Name = model.Name;
            existingRecord.Email = model.Email;
            existingRecord.Date = model.Date;

            _dbContext.SaveChanges();

            return NoContent();
        }


        [HttpGet]
        [Route("get/{id}", Name = "get student by Id")]
        //public ActionResult<StudentMod> GetStudentsById(int id)
        //{
        //    if (id <= 0)
        //    {
        //        //badRequest--400
        //        return BadRequest();

        //    }
        //    var txt = _dbContext.Student.Where(n => n.Id == id).FirstOrDefault();
        //    if (txt == null) 
        //    {
        //        return NotFound();

        //    }
        //    return Ok(txt);

        //}


        public ActionResult<List<Student>> getStudentById(int id) 
        {
            if (id <= 0)
            {
               return NoContent();
            }
            if(_dbContext.Student.FromSqlRaw($"EXEC GetStudentById {id}").ToList().IsNullOrEmpty() == true)
            {
                _logger.LogCritical("there is no given id in database");
                return NoContent();
            }
            return Ok();
        
        }


        [HttpDelete]
        [Route("delete/{id}", Name = "delete by id of students")]
        public ActionResult DeleteStudentsById(int id)
        {
            var stu = _dbContext.Student.Where(n => n.Id == id).FirstOrDefault();
            if (stu == null)
            {
                return NotFound();
            }
            _dbContext.Student.Remove(stu);
            _dbContext.SaveChanges();

            return Ok(stu);

        }

        [HttpGet]
        [Route("{name}", Name = "get by name students")]
        public ActionResult GetStudentByName(string name)
        {
            var txt = _dbContext.Student.Where(n => n.Name == name).FirstOrDefault();

            if(txt == null)
            {
                return BadRequest();
            }
            return Ok(txt);

        }
    }
}
