using Dominio;
using Microsoft.AspNetCore.Identity;

namespace Persistencia
{
    public class DataPrueba
    {
        public static async Task insertarData(CursosOnlineContext context, UserManager<Usuario> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new Usuario { NombreCompleto = "Jaime Perez", UserName = "jaipecas", Email = "jaipecas89@gmail.com" };
                await userManager.CreateAsync(user, "Pass123@");
            }
        }
    }
}
