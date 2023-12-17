namespace Clean.Architecture.Business.Features.Queries.Get;

public class GetAllSampleQuery : IRequest<List<SampleEntity>>
{
}

public class GetAllSampleQueryHandler : IRequestHandler<GetAllSampleQuery, List<SampleEntity>>
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllSampleQueryHandler> _logger;

    public GetAllSampleQueryHandler(
        AppDbContext dbContext,
        IMapper mapper,
        ILogger<GetAllSampleQueryHandler> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task<List<SampleEntity>> Handle(GetAllSampleQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
