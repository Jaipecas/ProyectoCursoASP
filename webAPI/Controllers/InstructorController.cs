
using App.Instructores.Queries;
using Microsoft.AspNetCore.Mvc;
using Persistencia.DapperConnection.Instructor;

namespace WebAPI.Controllers
{
    public class InstructorController : MyControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<InstructorModel>>> GetInstructores()
        {
            return await Mediator.Send(new GetInstructoresQuery());
        }
    }
}
