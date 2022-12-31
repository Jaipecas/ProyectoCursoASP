
using App.ErrorHandler;
using Dominio;
using MediatR;
using Persistencia;

namespace App.Cursos
{
    public class ConsultaId
    {
        public class CursoUnico : IRequest<Curso>
        {
            public int Id { get; set; }
        }

        public class Manejador : IRequestHandler<CursoUnico, Curso>
        {
            private readonly CursosOnlineContext _context;
            public Manejador(CursosOnlineContext context) => _context = context;
            public async Task<Curso> Handle(CursoUnico request, CancellationToken cancellationToken)
            {
                Curso? curso = await _context.Curso.FindAsync(request.Id);

                if (curso == null) throw new NotFoundException("CURSO");

                return curso;
            }
        }
    }
}
