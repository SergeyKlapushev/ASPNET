using Microsoft.Extensions.Caching.Memory;
using System.Text.RegularExpressions;
using sem1.Abstraction;
using sem1.Models;
using AutoMapper;
using sem1.Dto;

namespace sem1.Repo
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public ProductRepository(IMapper mapper, IMemoryCache cache)
        {
            _mapper = mapper;
            _cache = cache;
        }

        public int AddCategory(Category group)
        {
            using (var context = new ProductContext())
            {
                var entityGroup = context.Categories.FirstOrDefault(x => x.Name.ToLower() == group.Name.ToLower());
                if (entityGroup == null)
                {
                    entityGroup = _mapper.Map<Models.Category>(group);
                    context.Categories.Add(entityGroup);
                    context.SaveChanges();
                    _cache.Remove("category");
                }
                return entityGroup.Id;
            }
        }

        public int AddProduct(Product product)
        {
            using (var context = new ProductContext())
            {
                var entityProduct = context.Products.FirstOrDefault(x => x.Name.ToLower() == product.Name.ToLower());
                if (entityProduct == null)
                {
                    entityProduct = _mapper.Map<Models.Product>(product);
                    context.Products.Add(entityProduct);
                    context.SaveChanges();
                    _cache.Remove("products");
                }
                return entityProduct.Id;
            }
        }

        public IEnumerable<Category> GetCategory()
        {
            if (_cache.TryGetValue("category", out List<CategoryDto> category))
            {
                return (IEnumerable<Category>)category;

            }

            using (var context = new ProductContext())
            {
                var categoryList = context.Categories.Select(x => _mapper.Map<ProductContext>(x)).ToList();
                _cache.Set("category", categoryList, TimeSpan.FromMinutes(30));
                return (IEnumerable<Category>)categoryList;
            }
        }

        public IEnumerable<Product> GetProducts()
        {
            if (_cache.TryGetValue("products", out List<ProductDto> products))
            {
                return (IEnumerable<Product>)products;

            }
            using (var context = new ProductContext())
            {
                var productList = context.Products.Select(x => _mapper.Map<ProductDto>(x)).ToList();
                _cache.Set("product", productList, TimeSpan.FromMinutes(30));
                return (IEnumerable<Product>)products;

            }
        }
    }
}
