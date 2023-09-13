using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers;
[ApiController]
[Route("")]
public class HomeController : ControllerBase
{
    [HttpGet("")]
    public IActionResult Get()
    {
        return Ok();
    }
}
