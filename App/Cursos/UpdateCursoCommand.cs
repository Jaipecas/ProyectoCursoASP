
using Dominio;
using MediatR;
using Persistencia;

namespace App.Cursos
{
    public class UpdateCursoCommand : IRequest
    {
        public int cursoId { get; set; }
        public string? titulo { get; set; }
        public string? descripcion { get; set; }
        public DateTime? fechaPublicacion { get; set; }
    }

    public class UpdateCursoCommandHandler : IRequestHandler<UpdateCursoCommand>
    {
        private readonly CursosOnlineContext _context;

        public UpdateCursoCommandHandler (CursosOnlineContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCursoCommand request, CancellationToken cancellationToken)
        {
            Curso? curso = await _context.Curso.FindAsync(request.cursoId);
            int changes = 0;

            if (curso == null) throw new Exception("EL CURSO " + request.cursoId + " NO EXSITE");

            curso.titulo = request.titulo ?? curso.titulo;
            curso.descripcion = request.descripcion ?? curso.descripcion;
            curso.fechaPublicacion = request.fechaPublicacion ?? curso.fechaPublicacion;

            changes = await _context.SaveChangesAsync();

            if (changes > 0) return Unit.Value;

            throw new Exception("NO SE HA ACTUALIZADO EL REGISTRO");
        }
    }


}
