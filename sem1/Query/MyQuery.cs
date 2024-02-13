using sem1.Abstraction;
using sem1.Dto;

namespace sem1.Query
{
    public class MyQuery
    {
        public IEnumerable<ProductDto> GetProducts([Service] IProductServece service) => service.GetProducts();
        public IEnumerable<StorageDto> GetStorages([Service] IStorageService service) => service.GetStorage();
        public IEnumerable<CategoryDto> GetCategories([Service] ICategoryService service) => service.GetCategories();
    }
}
