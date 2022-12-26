using Dominio;
using Microsoft.AspNetCore.Mvc;
using Persistencia;

namespace webAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly CursosOnlineContext context;
        public WeatherForecastController(CursosOnlineContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Curso> Get()
        {
            return context.Curso.ToList();
        }
    }
}