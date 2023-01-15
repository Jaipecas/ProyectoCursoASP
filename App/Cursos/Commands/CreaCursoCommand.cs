using Azure.Core;
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
        public Precio precio { get; set; }
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
            var curso = AddCurso(request);
            AddInstructoresCurso(request, curso);
            AddPrecioCurso(request, curso);

            var changes = await _context.SaveChangesAsync();

            if (changes > 0) return Unit.Value;

            throw new Exception("NO SE HA CREADO EL CURSO");
        }

        private Curso AddCurso(CreaCurso request)
        {
            Guid cursoId = new Guid();
            Curso curso = new()
            {
                cursoId = cursoId,
                titulo = request.titulo,
                descripcion = request.descripcion,
                fechaPublicacion = request.fechaPublicacion,
            };

            _context.Curso.Add(curso);
            return curso;
        }

        private void AddInstructoresCurso(CreaCurso request, Curso curso)
        {
            if (request.instructores == null) return;

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

        private void AddPrecioCurso(CreaCurso request, Curso curso)
        {
            Precio precioCurso = new()
            {
                precioId = new Guid(),
                precioActual = request.precio.precioActual,
                promocion = request.precio.promocion,
                cursoId = curso.cursoId
            };

            _context.Precio.Add(precioCurso);
        }
    }
}
