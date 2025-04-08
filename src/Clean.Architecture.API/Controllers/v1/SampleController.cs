using Asp.Versioning;
using Clean.Architecture.Business.Common.Models;
using Clean.Architecture.Business.UserFeatures.CreateUser;
using Clean.Architecture.Business.UserFeatures.GetAllUser;
using Clean.Architecture.Business.UserFeatures.UpdateUser;
using Clean.Architecture.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Architecture.API.Controllers.v1;

[ApiVersion("1.0")]
public class SampleController : ApiControllerBase
{        
    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(List<SampleEntity>), StatusCodes.Status200OK)]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<ActionResult> GetAll()
    {
        return Ok(await Mediator.Send(new GetAllSampleQuery()));
    }

    [HttpPost]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<ActionResult> Add(CreateSampleCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("{id:int}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<ActionResult> Update(int id, UpdateSampleCommand command)
    {
        if (id != command.Id)
            return BadRequest();

        return Ok(await Mediator.Send(command));
    }
}
