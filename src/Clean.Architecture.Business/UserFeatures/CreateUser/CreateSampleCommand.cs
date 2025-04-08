using AutoMapper;
using Clean.Architecture.Business.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clean.Architecture.Business.UserFeatures.CreateUser;

public class CreateSampleCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class CreateSampleCommandHandler : IRequestHandler<CreateSampleCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateSampleCommandHandler> _logger;

    public CreateSampleCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<CreateSampleCommandHandler> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task<int> Handle(CreateSampleCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
