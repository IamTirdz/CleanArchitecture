namespace Clean.Architecture.Business.Features.Commands.Create;

public class CreateSampleCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class CreateSampleCommandHandler : IRequestHandler<CreateSampleCommand, int>
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateSampleCommandHandler> _logger;

    public CreateSampleCommandHandler(
        AppDbContext dbContext,
        IMapper mapper,
        ILogger<CreateSampleCommandHandler> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task<int> Handle(CreateSampleCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
