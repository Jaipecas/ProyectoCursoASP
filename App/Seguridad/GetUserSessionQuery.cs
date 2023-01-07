
using App.Interfaces;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace App.Seguridad
{
    public class GetUserSessionQuery : IRequest<UserData> { }

    public class GetUserSessionHandler : IRequestHandler<GetUserSessionQuery, UserData>
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IUserSession _userSession;
        private readonly IJwtGenerator _jwtGenerator;

        public GetUserSessionHandler(UserManager<Usuario> userManager, IUserSession userSession, IJwtGenerator jwtGenerator)
        {
            _userManager = userManager;
            _userSession = userSession;
            _jwtGenerator = jwtGenerator;
        }
        
        public async Task<UserData> Handle(GetUserSessionQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(_userSession.GetUserSession());

            return new UserData
            {
                NombreCompleto = user.NombreCompleto,
                Username = user.UserName,
                Token = _jwtGenerator.GenerateToken(user),
                Imagen = null,
                Email = user.Email
            };
        }
    }
}
