namespace Clean.Architecture.DataAccess.Entities
{
    public class Product : EntityBase
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
