using App.Cursos;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
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

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
