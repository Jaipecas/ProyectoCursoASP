using App.ErrorHandler;
using Dominio;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Cursos
{
    public class DeleteCursoCommand : IRequest
    {
        public int cursoId { get; set; }
    }

    public class DeleteCursoCommandHandler : IRequestHandler<DeleteCursoCommand>
    {
        private readonly CursosOnlineContext _context;

        public DeleteCursoCommandHandler(CursosOnlineContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCursoCommand request, CancellationToken cancellationToken)
        {
            Curso? curso = await _context.Curso.FindAsync(request.cursoId);
            int changes;

            if (curso == null) throw new NotFoundException("CURSO");

            _context.Remove(curso);

            changes = await _context.SaveChangesAsync();

            if (changes > 0) return Unit.Value;

            throw new Exception("NO SE HA BORRADO CORRECTAMENTE EL CURSO");
        }
    }

}
