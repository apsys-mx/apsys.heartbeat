using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apsys.heartbeat.webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : WebApiControllerBase
    {
        public HomeController(ILogger<WebApiControllerBase> logger, IMediator mediator, IWebHostEnvironment webHostEnvironment)
            : base(logger, mediator, webHostEnvironment)
        {
        }

        [HttpGet("greeting")]
        [AllowAnonymous]
        public IActionResult Get()
        {
            return Ok("Hello world from web api");
        }
    }
}