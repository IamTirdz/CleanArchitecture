namespace Clean.Architecture.DataAccess.Entities;

public class BaseEntity
{
    public long Id { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? Modified { get; set; }
}
