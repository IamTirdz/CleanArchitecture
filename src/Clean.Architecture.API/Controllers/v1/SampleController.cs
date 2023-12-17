using Clean.Architecture.Business.Features.Commands.Create;
using Clean.Architecture.Business.Features.Commands.Update;
using Clean.Architecture.Business.Features.Queries.Get;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Architecture.API.Controllers.v1
{
    [ApiVersion("1.0")]
    public class SampleController : ApiControllerBase
    {
        [MapToApiVersion("1.0")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllSampleQuery()));
        }

        [MapToApiVersion("1.0")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Add(CreateSampleCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [MapToApiVersion("1.0")]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update(int id, UpdateSampleCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            return Ok(await Mediator.Send(command));
        }
    }
}
