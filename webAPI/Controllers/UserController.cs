using App.Seguridad;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{

    public class UserController : MyControllerBase
    {
 
        // http://localhost:5115/api/User/login
        [HttpPost("login")]
        public async Task<ActionResult<UserData>> Login(Login login)
        {
            return await Mediator.Send(login);
        }
    }
}
