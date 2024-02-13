using sem1.Abstraction;
using sem1.Dto;

namespace sem1.Mutatin
{
    public class MyMutation
    {
        public int AddProduct(ProductDto product, [Service] IProductServece service)
        {
            var id = service.AddProduct(product);
            return id;
        }
    }
}
