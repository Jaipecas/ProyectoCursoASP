namespace App.Cursos.DTO
{
    public class CursoDTO
    {
        public Guid cursoId { get; set; }
        public string? titulo { get; set; }
        public string? descripcion { get; set; }
        public DateTime? fechaPublicacion { get; set; }
        public byte[]? fotoPortada { get; set; }
        public ICollection<InstructorDTO> instructors { get; set; }
        public PrecioDTO Precio { get; set; }
        public ICollection<ComentarioDTO> comentarios { get; set; }
    }
}
