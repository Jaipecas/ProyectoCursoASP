using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;

namespace App.Cursos.Commands
{
    public class CreaCurso : IRequest
    {
        public string? titulo { get; set; }
        public string? descripcion { get; set; }
        public DateTime? fechaPublicacion { get; set; }
        public List<Guid> instructores { get; set; }
    }

    public class CrearCursoValidations : AbstractValidator<CreaCurso>
    {
        public CrearCursoValidations()
        {
            RuleFor(x => x.titulo).NotEmpty();
            RuleFor(x => x.descripcion).NotEmpty();
        }
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
            Guid cursoId = new Guid();

            Curso curso = new()
            {
                cursoId = cursoId,
                titulo = request.titulo,
                descripcion = request.descripcion,
                fechaPublicacion = request.fechaPublicacion,
            };

            _context.Curso.Add(curso);

            if (request.instructores != null)
            {
                request.instructores.ForEach(instructorId =>
                {
                    var cursoInstructor = new CursoInstructor
                    {
                        cursoId = curso.cursoId,
                        instructorId = instructorId
                    };
                    _context.cursoInstructor.Add(cursoInstructor);
                });
            }

            changes = await _context.SaveChangesAsync();

            if (changes > 0) return Unit.Value;

            throw new Exception("NO SE HA CREADO EL CURSO");
        }
    }
}
