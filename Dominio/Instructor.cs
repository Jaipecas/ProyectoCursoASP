using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Instructor
    {
        public int InstructorId { get; set; }
        public string? Nombre { get; set; }
        public string? Apellidos { get; set; }
        public string? Grado { get; set; }
        public byte[]? Foto { get; set; }
        public ICollection<CursoInstructor>? CursoInstructors { get; set; }
    }
}
