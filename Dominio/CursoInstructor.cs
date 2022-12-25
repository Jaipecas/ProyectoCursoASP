
namespace Dominio
{
    public class CursoInstructor
    {
        public int cursoId { get; set; }
        public Curso? Curso { get; set; }
        public int instructorId { get; set; }
        public Instructor? Instructor { get; set; }
    }
}
