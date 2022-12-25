using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Curso
    {
        public int cursoId { get; set; }
        public string? titulo { get; set; }
        public string? descripcion { get; set; }
        public DateTime fechaPublicacion { get; set; }
        public byte[]? fotoPortada { get; set; }
        public Precio? precio { get; set; }
        public ICollection<Comentario>? Comentarios { get; set; }
        public ICollection<CursoInstructor>? CursoInstructors { get; set; }
    }
}
