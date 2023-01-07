using App.Cursos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{

    public class CursosController : MyControllerBase
    {
        // http://localhost:5115/api/Cursos
        [HttpGet]
        public async Task<ActionResult<List<Curso>>> Get() => await Mediator.Send(new Consulta.ListaCursos());

        // http://localhost:5115/api/Cursos/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> Detalle(int id) => await Mediator.Send(new ConsultaId.CursoUnico { Id = id });

        // http://localhost:5115/api/Cursos/
        [HttpPost]
        public async Task<ActionResult<Unit>> CreaCurso(CreaCurso data) => await Mediator.Send(data);

        // http://localhost:5115/api/Cursos/3
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> UpdateCurso(int id, UpdateCursoCommand data) => await Mediator.Send(new UpdateCursoCommand { cursoId = id });

        // http://localhost:5115/api/Cursos/3
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteCurso(int id) => await Mediator.Send(new DeleteCursoCommand { cursoId = id });
    }
}
