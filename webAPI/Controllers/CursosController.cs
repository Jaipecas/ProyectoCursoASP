using App.Cursos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{

    [Route("api/Cursos")]
    [ApiController]
    public class CursosController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CursosController(IMediator mediator) => _mediator = mediator;

        // http://localhost:5115/api/Cursos
        [HttpGet]
        public async Task<ActionResult<List<Curso>>> Get() => await _mediator.Send(new Consulta.ListaCursos());

        // http://localhost:5115/api/Cursos/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> Detalle(int id) => await _mediator.Send(new ConsultaId.CursoUnico { Id = id });

        // http://localhost:5115/api/Cursos/
        [HttpPost]
        public async Task<ActionResult<Unit>> CreaCurso(CreaCurso data) => await _mediator.Send(data);

        // http://localhost:5115/api/Cursos/3
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> UpdateCurso(int id, UpdateCursoCommand data) => await _mediator.Send(new UpdateCursoCommand { cursoId = id });

        // http://localhost:5115/api/Cursos/3
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteCurso(int id) => await _mediator.Send(new DeleteCursoCommand { cursoId = id });
    }
}
