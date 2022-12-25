using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Precio
    {
        public int precioId { get; set; }
        public decimal precioActual { get; set; }
        public decimal promocion { get; set; }
        public int cursoId { get; set; }
        public Curso? curso { get; set; }
      
    }
}
