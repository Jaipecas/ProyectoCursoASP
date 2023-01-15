using App.Cursos.DTO;
using App.ErrorHandler;
using AutoMapper;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace App.Cursos.Queries
{
    public class GetCursoQuery : IRequest<CursoDTO>
    {
        public Guid Id { get; set; }
    }

    public class GetCursoQueryHandler : IRequestHandler<GetCursoQuery, CursoDTO>
    {
        private readonly CursosOnlineContext _context;
        private readonly IMapper _mapper;

        public GetCursoQueryHandler(CursosOnlineContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CursoDTO> Handle(GetCursoQuery request, CancellationToken cancellationToken)
        {
            var curso = await _context.Curso
                .Include(curso => curso.CursoInstructors)
                .ThenInclude(cursoInstructor => cursoInstructor.Instructor)
                .Include(curso => curso.precio)
                .Include(curso => curso.Comentarios)
                .FirstOrDefaultAsync(curso => curso.cursoId == request.Id);

            if (curso == null) throw new NotFoundException("CURSO");

            var cursoDTO = _mapper.Map<Curso, CursoDTO>(curso);

            return cursoDTO;
        }
    }

}
