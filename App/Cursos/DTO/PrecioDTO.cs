
namespace App.Cursos.DTO
{
    public class PrecioDTO
    {
        public Guid precioId { get; set; }
        public decimal precioActual { get; set; }
        public decimal promocion { get; set; }
        public Guid cursoId { get; set; }
    }
}
