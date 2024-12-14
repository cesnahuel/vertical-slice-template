using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VerticalSliceTemplate.Common
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        private ISender? _mediator;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>()!;
    }
}
