namespace Application.Models
{
    public class CategoryResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ICollection<ProductResponse>? Products { get; set; }
    }
}
