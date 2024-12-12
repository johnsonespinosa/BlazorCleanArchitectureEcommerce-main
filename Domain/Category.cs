using Domain.Commons;

namespace Domain.Models
{
    public class Category : BaseAuditableEntity
    {
        public string? Name { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
