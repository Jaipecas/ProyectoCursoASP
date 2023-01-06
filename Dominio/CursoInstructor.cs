
namespace Dominio
{
    public class CursoInstructor
    {
        public Guid cursoId { get; set; }
        public Curso? Curso { get; set; }
        public Guid instructorId { get; set; }
        public Instructor? Instructor { get; set; }
    }
}
