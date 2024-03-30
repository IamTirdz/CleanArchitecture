namespace Clean.Architecture.DataAccess.Entities
{
    public class Product : EntityBase
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
