using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Employee.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseController : ControllerBase
    {
        private readonly ILogger _logger;

        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        public BaseController(ILogger logger)
        {
            _logger = logger;
        }
    }
}