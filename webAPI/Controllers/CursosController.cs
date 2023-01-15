using App.Cursos.Commands;
using App.Cursos.DTO;
using App.Cursos.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{

    public class CursosController : MyControllerBase
    {
        // http://localhost:5115/api/Cursos
        [HttpGet]
        public async Task<ActionResult<List<CursoDTO>>> Get() => await Mediator.Send(new GetCursosQuery());

        // http://localhost:5115/api/Cursos/1
        [HttpGet("{id}")]
        public async Task<ActionResult<CursoDTO>> Detalle(Guid id) => await Mediator.Send(new GetCursoQuery{ Id = id });

        // http://localhost:5115/api/Cursos/
        [HttpPost]
        public async Task<ActionResult<Unit>> CreaCurso(CreaCurso data) => await Mediator.Send(data);

        // http://localhost:5115/api/Cursos/3
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> UpdateCurso(Guid id, UpdateCursoCommand data) {
            data.cursoId = id;
            return await Mediator.Send(data);
        } 

        // http://localhost:5115/api/Cursos/3
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteCurso(Guid id) => await Mediator.Send(new DeleteCursoCommand { cursoId = id });
    }
}
