using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Architecture.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class ApiControllerBase : ControllerBase
{
<<<<<<< HEAD
    private IMediator _mediator = null!;
    public IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
=======
    private ISender _mediator = null!;
    private IMapper _mapper = null!;

    public ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    public IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetRequiredService<IMapper>();
>>>>>>> update template
}
