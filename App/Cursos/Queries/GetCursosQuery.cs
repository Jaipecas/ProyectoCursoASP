using App.Cursos.DTO;
using AutoMapper;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace App.Cursos.Queries
{
    public class GetCursosQuery : IRequest<List<CursoDTO>> { }

    public class GetCursosQueryHandler : IRequestHandler<GetCursosQuery, List<CursoDTO>>
    {
        private readonly CursosOnlineContext _context;
        private readonly IMapper _mapper;

        public GetCursosQueryHandler(CursosOnlineContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CursoDTO>> Handle(GetCursosQuery request, CancellationToken cancellationToken)
        {
            var cursos = await _context.Curso
                .Include(x => x.CursoInstructors)
                .ThenInclude(x => x.Instructor)
                .ToListAsync();

            var cursosDTO = _mapper.Map<List<Curso>, List<CursoDTO>>(cursos);

            return cursosDTO;
        }
    }
}

