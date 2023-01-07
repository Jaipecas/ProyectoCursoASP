
using Dominio;

namespace App.Interfaces
{
    public interface IJwtGenerator
    {
        string GenerateToken(Usuario usuario);
    }
}
