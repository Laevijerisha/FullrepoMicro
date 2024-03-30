using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using E_wasteManagementWebapi.Data;
using E_wasteManagementWebapi.Model;
using Microsoft.AspNetCore.Identity.Data;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Text.RegularExpressions;
using Org.BouncyCastle.Crypto.Macs;
using E_wasteManagementWebapi.DTO;
using Microsoft.EntityFrameworkCore;
namespace E_wasteManagementWebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly E_WasteDbContext _context;
    

        public LoginController(E_WasteDbContext context)
        {
            _context = context;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO login)
        {

            var user = _context.users.FirstOrDefault(u => u.Email == login.Email && u.Password == login.Password);
            if (user == null)
            {
                return NotFound("Invalid Username or password");

            }
            if (user.Email != login.Email && user.Password != login.Password)
            {
                return Unauthorized("Invalid User Id");
            }
            return Ok();

            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [ HttpPost("signup")]
public IActionResult Signup([FromBody] User usersignup)
        {
            //var existingUser=_context.users.FirstOrDefault(u=>u.Email==usersignup.Email);
            //if (existingUser == null)
            //{
            //    return Conflict("User Already exists");
            //}
            _context.users.Add(usersignup);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Signup), new { id = usersignup.UserId }, usersignup);

        }

        //find details using email
        [HttpPost("User/cookie")]
        public ActionResult FindEmail([FromBody] UserEmail email)
        {
            var Email = _context.users.Where(s => s.Email == email.Email).FirstOrDefault();
            if (Email == null)
            {
                return Ok("Not the data");
            }
            else
            {
                return Ok(Email);
            }
        }


    }
}
