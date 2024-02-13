using sem1.Dto;

namespace sem1.Abstraction
{
    public interface IProductServece
    {
        public int AddProduct(ProductDto product);

        public IEnumerable<ProductDto> GetProducts();
    }
}
