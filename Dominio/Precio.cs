using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Precio
    {
        public Guid precioId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal precioActual { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal promocion { get; set; }
        public Guid cursoId { get; set; }
        public Curso? curso { get; set; }

    }
}
