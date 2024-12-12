namespace Domain.Entities
{
    public class Category
    {
        public string? Name { get; set; }
        public IEnumerable<Product>? Products { get; set; }
    }
}
