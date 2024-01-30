namespace sem1.Models
{
    public class Category : BaseModelProduct
    {
        public virtual List<Product> Products { get; set; } = new List<Product>();
    }
}
