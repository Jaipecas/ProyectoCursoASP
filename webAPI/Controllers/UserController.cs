using App.Seguridad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [AllowAnonymous]
    public class UserController : MyControllerBase
    {
        // http://localhost:5115/api/User/login
        [HttpPost("login")]
        public async Task<ActionResult<UserData>> Login(Login login)
        {
            return await Mediator.Send(login);
        }

        // http://localhost:5115/api/User/register
        [HttpPost("register")]
        public async Task<ActionResult<UserData>> RegisterUser(CreateUserCommand createUserCommand)
        {
            return await Mediator.Send(createUserCommand);
        }

        // http://localhost:5115/api/User
        [HttpGet]
        public async Task<ActionResult<UserData>> GetUserSession()
        {
            return await Mediator.Send(new GetUserSessionQuery());
        }

    }
}
