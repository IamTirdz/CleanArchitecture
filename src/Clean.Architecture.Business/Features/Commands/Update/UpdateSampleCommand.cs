namespace Clean.Architecture.Business.Features.Commands.Update;

public class UpdateSampleCommand : IRequest<int>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class UpdateSampleCommandHandler : IRequestHandler<UpdateSampleCommand, int>
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateSampleCommandHandler> _logger;

    public UpdateSampleCommandHandler(
        AppDbContext dbContext,
        IMapper mapper,
        ILogger<UpdateSampleCommandHandler> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task<int> Handle(UpdateSampleCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
