using App.Cursos.DTO;
using AutoMapper;
using Dominio;

namespace App
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Curso, CursoDTO>()
                .ForMember(x => x.instructors, y=> y.MapFrom( z => z.CursoInstructors.Select(a => a.Instructor).ToList()));

            CreateMap<CursoInstructor, CursoInstructorDTO>();

            CreateMap<Instructor, InstructorDTO>();
        }
    }
}
