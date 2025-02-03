using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AuthBackend.Models;
using System.Threading.Tasks;

namespace AuthBackend.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly AppDbContext _dbContext;

        
        public AuthController(AppDbContext dbContext) 
        {
            _dbContext = dbContext;
        }


        [HttpPost]
        [Route("create-Student")]
        public ActionResult createStudent([FromBody] User user)
        {
            if (user == null)
            {
                return NoContent();
            }

            _dbContext.Add(user);

            return Ok(_dbContext.SaveChanges());
        }
    }
    
}
