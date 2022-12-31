
using System.Net;

namespace App.ErrorHandler
{
    public class NotFoundException : Exception
    {
        public static HttpStatusCode StatusCode { get { return HttpStatusCode.NotFound; } }
        public object Errores { get; }

        public NotFoundException(string message)
        {
            Errores = new { mensaje = "NO SE ENCONTRO EL " + message + "" };
        }
    }
}
