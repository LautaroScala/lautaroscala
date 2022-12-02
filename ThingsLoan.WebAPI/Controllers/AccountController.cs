using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThingsLoan.WebAPI.DataAccess;
using ThingsLoan.WebAPI.DTO;
using ThingsLoan.WebAPI.Entities;
using ThingsLoan.WebAPI.Handlers;

namespace ThingsLoan.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IJwtHandler jwtHandler;
        public AccountController(IJwtHandler jwtHandler, IUnitOfWork uow)
        {
            this.jwtHandler = jwtHandler;
            this.uow = uow;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody]UserLoginDto user)
        {
            var dbUser = uow.PersonRepository.GetPersonByUsername(user.Username);
            if (dbUser is null)
            {
                return BadRequest("Username Non Existant");
            }
            if (dbUser.Password != user.Password)
            {
                return StatusCode(406);
            }
            var bearer = jwtHandler.GenerateJWT(
                new UserLoginDto { Username = dbUser.Username, Password = dbUser.Password },
                new List<string> { dbUser.Role }
                );
            return Ok(new { token = bearer });
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody]PersonDto user) 
        {
            var dbUser = uow.PersonRepository.GetPersonByUsername(user.Username);
            if (dbUser is null)
            {
                var newuser = new Person
                {
                    Id = 0,
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    Phone = user.Phone,
                    Username = user.Username,
                    Role = "User"
                };
                if (user.Username == "admin")
                {
                    newuser.Role= "Admin";
                }
                uow.PersonRepository.Add(newuser);
                uow.Complete();
                return Ok("User created");
            }
            return BadRequest();
        }
    }
}
