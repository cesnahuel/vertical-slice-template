using MediatR;
using Microsoft.AspNetCore.Mvc;
using VerticalSliceTemplate.Common;

namespace VerticalSliceTemplate.Features.Taxes.Command.CreateTax
{
    [Route("api/CreateTaxControl")]
    public class CreateTaxController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<CreateTaxResponse>> Create([FromBody] CreateTaxCommand command)
        {
            return await Mediator.Send(command, new CancellationToken());
        }
    }
}
