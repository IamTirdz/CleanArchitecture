using AutoMapper;
using Clean.Architecture.Business.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clean.Architecture.Business.UserFeatures.UpdateUser;

public class UpdateSampleCommand : IRequest<int>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class UpdateSampleCommandHandler : IRequestHandler<UpdateSampleCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateSampleCommandHandler> _logger;

    public UpdateSampleCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<UpdateSampleCommandHandler> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task<int> Handle(UpdateSampleCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
