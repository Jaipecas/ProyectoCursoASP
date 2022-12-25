using Microsoft.AspNetCore.Mvc;

namespace webAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            string[] nombres = new[]
            {
                "Fabian",
                "Rolando",
                "María",
                "Rebeca"
            };
            return nombres;
        }
    }
}