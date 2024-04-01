namespace Clean.Architecture.Business.Products.Queries.GetProducts
{
    public class ProductDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
