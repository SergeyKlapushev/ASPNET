namespace sem1.Models
{
    public class Storage : BaseModelProduct
    {
        public int Count { get; set; }
        public virtual List<Product> Products { get; set; } = null;
    }
}
