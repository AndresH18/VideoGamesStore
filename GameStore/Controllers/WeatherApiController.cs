using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherApiController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hola");
    }
}