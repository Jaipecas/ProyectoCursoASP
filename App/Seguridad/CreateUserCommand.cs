
using App.ErrorHandler;
using App.Interfaces;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace App.Seguridad
{
    public class CreateUserCommand : IRequest<UserData>
    {
        public string? Nombre { get; set; }
        public string? Apellidos { get; set; }
        public string? Email { get; set; }
        public string? Pass { get; set; }
        public string? UserName { get; set; }
    }

    public class CreateUserCommandValidations : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidations()
        {
            RuleFor(x => x.Nombre).NotEmpty();
            RuleFor(x => x.Apellidos).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Pass).NotEmpty();
            RuleFor(x => x.UserName).NotEmpty();
        }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserData>
    {
        private readonly CursosOnlineContext _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly IJwtGenerator _jwtGenerator;

        public CreateUserCommandHandler(CursosOnlineContext context, UserManager<Usuario> userManager, IJwtGenerator jwtGenerator)
        {
            _context = context;
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<UserData> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existEmail = await _context.Users.Where(x => x.Email == request.Email).AnyAsync();

            if (existEmail) throw new ExistException("EMAIL", request.Email ?? "");

            var existUser = await _context.Users.Where(x => x.UserName == request.UserName).AnyAsync();

            if (existUser) throw new ExistException("USUARIO", request.UserName ?? "");

            var user = new Usuario
            {
                NombreCompleto = request.Nombre + " " + request.Apellidos,
                Email = request.Email,
                UserName = request.UserName,
            };

            var result = await _userManager.CreateAsync(user, request.Pass);

            if (result.Succeeded)
            {
                return new UserData
                {
                    NombreCompleto = user.NombreCompleto,
                    Token = _jwtGenerator.GenerateToken(user),
                    Email = user.Email,
                    Username = user.UserName,
                    Imagen = null
                };
            }

            throw new Exception("NO SE HA PODIDO CREAR EL USUARIO");
        }
    }
}
