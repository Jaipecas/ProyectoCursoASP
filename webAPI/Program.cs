using App.Cursos;
using Dominio;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Persistencia;
using WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<CursosOnlineContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddMediatR(typeof(Consulta.Manejador).Assembly);
builder.Services.AddControllers().AddFluentValidation(cfr => cfr.RegisterValidatorsFromAssemblyContaining<CreaCurso>());

//configuración core Identity
var builderIdentity = builder.Services.AddIdentityCore<Usuario>();
var identuty = new IdentityBuilder(builderIdentity.UserType, builderIdentity.Services);
identuty.AddEntityFrameworkStores<CursosOnlineContext>();

//LOGIN
identuty.AddSignInManager<SignInManager<Usuario>>();
builder.Services.TryAddSingleton<ISystemClock, SystemClock>();

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
        DataPrueba.insertarData(context, userManager).Wait();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
