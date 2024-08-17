using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public abstract class ApiControllerBase : ControllerBase
    {
        private ISender _mediator = null!;
        private IMediator? _publisher = null;
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
        protected IMediator Publisher => _publisher ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
       
    }
}