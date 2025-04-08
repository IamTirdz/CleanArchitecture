namespace Clean.Architecture.DataAccess.Entities;

public class SampleEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
}
