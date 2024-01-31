using sem1.Repo;
using sem1.Models;

namespace sem1.Abstraction
{
    public interface IProductRepository
    {
        public int AddCategory(Category group);

        public IEnumerable<Category> GetCategory();

        public int AddProduct(Product product);

        public IEnumerable<Product> GetProducts();
    }
}
