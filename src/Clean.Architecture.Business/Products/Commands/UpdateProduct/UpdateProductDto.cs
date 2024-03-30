namespace Clean.Architecture.Business.Products.Commands.UpdateProduct
{
    public class UpdateProductDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
