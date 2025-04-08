using AutoMapper;
using Clean.Architecture.Business.Repositories;
using Clean.Architecture.DataAccess.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clean.Architecture.Business.UserFeatures.GetAllUser;

public class GetAllSampleQuery : IRequest<List<SampleEntity>>
{
}

public class GetAllSampleQueryHandler : IRequestHandler<GetAllSampleQuery, List<SampleEntity>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllSampleQueryHandler> _logger;

    public GetAllSampleQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<GetAllSampleQueryHandler> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task<List<SampleEntity>> Handle(GetAllSampleQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
