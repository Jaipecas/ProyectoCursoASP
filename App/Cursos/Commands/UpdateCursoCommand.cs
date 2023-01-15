using App.ErrorHandler;
using Azure.Core;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace App.Cursos.Commands
{
    public class UpdateCursoCommand : IRequest
    {
        public Guid cursoId { get; set; }
        public string? titulo { get; set; }
        public string? descripcion { get; set; }
        public DateTime? fechaPublicacion { get; set; }
        public List<Guid> instructores { get; set; }
        public Precio precio { get; set; }

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

                if (curso == null) throw new NotFoundException("CURSO");

                Precio? precioCurso = await _context.Precio.FirstOrDefaultAsync(precio => precio.cursoId == curso.cursoId);

                UpdateDataCurso(curso, request);
                UpdatePrecioCurso(curso, precioCurso, request);
                UpdateCursoInstructor(curso, request);

                var changes = await _context.SaveChangesAsync();

                if (changes > 0) return Unit.Value;

                throw new Exception("NO SE HA ACTUALIZADO EL REGISTRO");
            }

            private void UpdateDataCurso(Curso curso, UpdateCursoCommand request)
            {
                curso.titulo = request.titulo ?? curso.titulo;
                curso.descripcion = request.descripcion ?? curso.descripcion;
                curso.fechaPublicacion = request.fechaPublicacion ?? curso.fechaPublicacion;
            }

            private void UpdatePrecioCurso(Curso curso, Precio precioCurso, UpdateCursoCommand request)
            {
                if (precioCurso != null)
                {
                    precioCurso.precioActual = request.precio.precioActual ?? precioCurso.precioActual;
                    precioCurso.promocion = request.precio.promocion ?? precioCurso.promocion;
                }
                else
                {
                    _context.Precio.Add(new Precio
                    {
                        precioId = new Guid(),
                        precioActual = request.precio.precioActual,
                        promocion = request.precio.promocion,
                        cursoId = curso.cursoId,
                    });
                }
            }

            private void UpdateCursoInstructor(Curso curso, UpdateCursoCommand request)
            {
                if (request.instructores != null && request.instructores.Count > 0)
                {
                    var instructoresCurso = _context.cursoInstructor.Where(cursoInstructor => cursoInstructor.cursoId == request.cursoId).ToList();
                    instructoresCurso.ForEach(instructoresCurso => _context.cursoInstructor.Remove(instructoresCurso));
                    request.instructores.ForEach(instructorId => _context.cursoInstructor.Add(new CursoInstructor { cursoId = request.cursoId, instructorId = instructorId }));
                }
            }
        }
    }
}
