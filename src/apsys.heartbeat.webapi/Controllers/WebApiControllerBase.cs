using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace apsys.heartbeat.webapi.Controllers
{
    public class WebApiControllerBase : Controller
    {
        protected internal readonly ILogger<WebApiControllerBase> _logger;
        protected internal readonly IWebHostEnvironment _webHostEnvironment;
        protected internal readonly IMediator _mediator;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mediator"></param>
        public WebApiControllerBase(ILogger<WebApiControllerBase> logger, IMediator mediator, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _mediator = mediator;
            _webHostEnvironment = webHostEnvironment;
        }
    }
}