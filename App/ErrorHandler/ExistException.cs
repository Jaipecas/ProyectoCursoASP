using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.ErrorHandler
{
    public class ExistException : Exception
    {
        public static HttpStatusCode StatusCode { get { return HttpStatusCode.BadRequest; } }
        public object Errores { get; }

        public ExistException(string type, string text)
        {
            Errores = new { mensaje = $"EL {0} {1} YA EXISTE", type, text };
        }
    }
}
