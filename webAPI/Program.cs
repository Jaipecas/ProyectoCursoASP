using App.Cursos.Commands;
using App.Cursos.Queries;
using App.Interfaces;
using Dominio;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Persistencia;
using Persistencia.DapperConnection;
using Persistencia.DapperConnection.Instructor;
using Seguridad;
using Seguridad.Token;
using System.Text;
using WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<CursosOnlineContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Conexion Dapper para manejar los procedimientos almacenados
builder.Services.AddOptions();
// Aqui se mapea el apartado connectionsStrings y se mete en nuestro objeto ConnectionConf
builder.Services.Configure<ConnectionConf>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.AddTransient<IFactoryConnection, FactoryConnection>();
builder.Services.AddScoped<IInstructores, InstructorRepositorio>();


builder.Services.AddMediatR(typeof(GetCursosQueryHandler).Assembly);

builder.Services.AddControllers(opt =>
{
    // Se añade a los controllers que el usuario tenga que estar autenticado para realizar la llamada
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
})
    .AddFluentValidation(cfr => cfr.RegisterValidatorsFromAssemblyContaining<CreaCurso>());

//configuración core Identity
var builderIdentity = builder.Services.AddIdentityCore<Usuario>();
var identuty = new IdentityBuilder(builderIdentity.UserType, builderIdentity.Services);
identuty.AddEntityFrameworkStores<CursosOnlineContext>();

//LOGIN
identuty.AddSignInManager<SignInManager<Usuario>>();
builder.Services.TryAddSingleton<ISystemClock, SystemClock>();

//TOKEN
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Mi palabra secreta"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = key,
        ValidateAudience = false,
        ValidateIssuer = false
    };
});
builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();

//interfaz y clase para manejar la la sesion de usuario
builder.Services.AddScoped<IUserSession, UserSession>();

//MAPEADO DE CLASES DTO
builder.Services.AddAutoMapper(typeof(GetCursosQueryHandler));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




var app = builder.Build();

//Se realiza la migración de entity a sqlserver
using (var ambiente = app.Services.CreateScope())
{
    var services = ambiente.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<Usuario>>();
        var context = services.GetRequiredService<CursosOnlineContext>();
        context.Database.Migrate();
        //DataPrueba.insertarData(context, userManager).Wait();
    }
    catch (Exception e)
    {
        var logging = services.GetRequiredService<ILogger<Program>>();
        logging.LogError(e, "ERROR EN MIGRACION");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Usamos nuestro middleware personalizado
app.UseMiddleware<ErrorHandlerMiddleware>();

//app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
