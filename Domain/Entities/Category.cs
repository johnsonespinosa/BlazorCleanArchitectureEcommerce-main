using Domain.Commons;

namespace Domain.Entities
{
    public class Category : BaseAuditableEntity
    {
        public string? Name { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
