using App.ErrorHandler;
using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;

namespace App.Cursos.Commands
{
    public class UpdateCursoCommand : IRequest
    {
        public int cursoId { get; set; }
        public string? titulo { get; set; }
        public string? descripcion { get; set; }
        public DateTime? fechaPublicacion { get; set; }
    }

    public class UpdateCursoCommandValidations : AbstractValidator<UpdateCursoCommand>
    {
        public UpdateCursoCommandValidations()
        {
            RuleFor(x => x.titulo).NotEmpty();
            RuleFor(x => x.descripcion).NotEmpty();
            RuleFor(x => x.fechaPublicacion).NotEmpty();
        }
    }

    public class UpdateCursoCommandHandler : IRequestHandler<UpdateCursoCommand>
    {
        private readonly CursosOnlineContext _context;

        public UpdateCursoCommandHandler(CursosOnlineContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCursoCommand request, CancellationToken cancellationToken)
        {
            Curso? curso = await _context.Curso.FindAsync(request.cursoId);
            int changes = 0;

            if (curso == null) throw new NotFoundException("CURSO");

            curso.titulo = request.titulo ?? curso.titulo;
            curso.descripcion = request.descripcion ?? curso.descripcion;
            curso.fechaPublicacion = request.fechaPublicacion ?? curso.fechaPublicacion;

            changes = await _context.SaveChangesAsync();

            if (changes > 0) return Unit.Value;

            throw new Exception("NO SE HA ACTUALIZADO EL REGISTRO");
        }
    }


}
