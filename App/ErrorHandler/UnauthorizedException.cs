

using System.Net;

namespace App.ErrorHandler
{
    public class UnauthorizedException : Exception
    {
        public static HttpStatusCode StatusCode { get { return HttpStatusCode.Unauthorized; } }
        public object Errores { get; }

        public UnauthorizedException()
        {
            Errores = new { mensaje = "USUARIO NO AUTORIZADO" };
        }
    }
}
