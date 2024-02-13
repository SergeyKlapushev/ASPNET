using System.Data.SqlTypes;

namespace sem1.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Cost { get; set; }
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public virtual List<Storage>? Storages { get; set; } = new List<Storage>();
    }
}
