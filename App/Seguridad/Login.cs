﻿
using App.Cursos;
using App.ErrorHandler;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace App.Seguridad
{
    public class Login : IRequest<UserData>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginValidations : AbstractValidator<Login>
    {
        public LoginValidations()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }

    public class LoginHandler : IRequestHandler<Login, UserData>
    {
        private readonly UserManager<Usuario> _userManger;
        private readonly SignInManager<Usuario> _signInManger;

        public LoginHandler(UserManager<Usuario> userManger, SignInManager<Usuario> signInManger)
        {
            _userManger = userManger;
            _signInManger = signInManger;
        }

        public async Task<UserData> Handle(Login request, CancellationToken cancellationToken)
        {
            var usuario = await _userManger.FindByEmailAsync(request.Email);

            if (usuario == null)
            {
                throw new NotFoundException("USUARIO");
            }

            var result = await _signInManger.CheckPasswordSignInAsync(usuario, request.Password, false);

            if (result.Succeeded)
            {
                return new UserData
                {
                    NombreCompleto = usuario.NombreCompleto,
                    Token = "AUN NO HEMOS IMPLEMENTADO LA LOGICA DEL TOKEN",
                    Username = usuario.UserName,
                    Email = usuario.Email,
                    Imagen = null
                };
            }

            throw new UnauthorizedException();
        }
    }
}
