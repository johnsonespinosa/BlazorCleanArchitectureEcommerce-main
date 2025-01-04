namespace Application.Commons.Models
{
    public class ProductResponse
    {
        public Guid Id { get; init; }
        public CategoryResponse? Category { get; init; }
        public string? Name { get; init; }
        public string? Description { get; init; }
        public string? ImageUrl { get; init; }
        public decimal Price { get; init; }
        public int Stock { get; init; }
    }
}
