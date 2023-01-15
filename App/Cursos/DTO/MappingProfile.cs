using AutoMapper;
using Dominio;

namespace App.Cursos.DTO
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Curso, CursoDTO>()
                .ForMember(cursoDto => cursoDto.instructors, conf => conf.MapFrom(curso => curso.CursoInstructors.Select(cursoInstructor => cursoInstructor.Instructor).ToList()))
                .ForMember(cursoDto => cursoDto.comentarios, conf => conf.MapFrom(curso => curso.Comentarios))
                .ForMember(cursoDto => cursoDto.Precio, conf => conf.MapFrom(curso => curso.precio));

            CreateMap<CursoInstructor, CursoInstructorDTO>();
            CreateMap<Instructor, InstructorDTO>();
            CreateMap<Precio, PrecioDTO>();
            CreateMap<Comentario, ComentarioDTO>();
        }
    }
}
