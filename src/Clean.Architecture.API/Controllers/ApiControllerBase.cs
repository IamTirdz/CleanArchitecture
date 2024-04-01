using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Architecture.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class ApiControllerBase : ControllerBase
{
    private IMediator _mediator = null!;
    public IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
}
