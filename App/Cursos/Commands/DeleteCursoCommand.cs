using App.ErrorHandler;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Cursos.Commands
{
    public class DeleteCursoCommand : IRequest
    {
        public Guid cursoId { get; set; }
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
            /* Eliminar los datos de la tabla intermedia (ni el registro de la tabla precio, ni los comentario) en realidad no hace falta ya que tenemos CONSTRAINT ON DELETE CASCADE
             * pero lo dejamoe a modo de ejemplo */

            var cursoTask = _context.Curso.Where(curso => curso.cursoId == request.cursoId).FirstOrDefaultAsync();
            var cursosIntructoresTask = _context.cursoInstructor.Where(cursoInstructor => cursoInstructor.cursoId == request.cursoId).ToListAsync();

            await Task.WhenAll(cursoTask, cursosIntructoresTask);

            var curso = await cursoTask;
            var cursosIntructores = await cursosIntructoresTask;

            if (cursosIntructores != null && cursosIntructores.Count > 0)
            {
                cursosIntructores.ForEach(cursosIntructores => _context.cursoInstructor.Remove(cursosIntructores));
            }

            if (curso == null) throw new NotFoundException("CURSO");

            _context.Remove(curso);

            var changes = await _context.SaveChangesAsync();

            if (changes > 0) return Unit.Value;

            throw new Exception("NO SE HA BORRADO CORRECTAMENTE EL CURSO");
        }
    }

}
