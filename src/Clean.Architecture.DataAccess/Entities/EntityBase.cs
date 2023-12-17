namespace Clean.Architecture.DataAccess.Entities;

public class EntityBase
{
    public long Id { get; set; }
    public DateTime Created { get; set; } = DatTime.Now;
    public DateTime? Modified { get; set; }
}
