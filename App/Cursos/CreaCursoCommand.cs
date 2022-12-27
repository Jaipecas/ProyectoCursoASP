using Dominio;
using MediatR;
using Persistencia;

namespace App.Cursos
{
    public class CreaCurso : IRequest
    {
        public string? titulo { get; set; }
        public string? descripcion { get; set; }
        public DateTime fechaPublicacion { get; set; }
    }

    public class HandlerCreaCurso : IRequestHandler<CreaCurso>
    {
        private readonly CursosOnlineContext _context;
        public HandlerCreaCurso(CursosOnlineContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(CreaCurso request, CancellationToken cancellationToken)
        {
            int changes;
            Curso curso = new()
            {
                titulo = request.titulo,
                descripcion = request.descripcion,
                fechaPublicacion = request.fechaPublicacion,
            };

            _context.Curso.Add(curso);
            changes = await _context.SaveChangesAsync();

            if (changes > 0) return Unit.Value;

            throw new Exception("NO SE HA CREADO EL CURSO");
        }
    }
}
