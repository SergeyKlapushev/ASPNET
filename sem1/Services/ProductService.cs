using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using sem1.Abstraction;
using sem1.Dto;
using sem1.Models;
using System;

namespace sem1.Services
{
    public class ProductService : IProductServece
    {
        private readonly ProductStorageDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public ProductService(ProductStorageDbContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }

        public int AddProduct(ProductDto product)
        {
            using (_context)
            {
                var entity = _mapper.Map<Product>(product);

                _context.Products.Add(entity);
                _context.SaveChanges();
                _cache.Remove("products");

                return entity.Id;
            }
        }

        IEnumerable<ProductDto> IProductServece.GetProducts()
        {
            using (_context)
            {
                if (_cache.TryGetValue("products", out List<ProductDto> products))
                    return products;

                products = _context.Products.Select(x => _mapper.Map<ProductDto>(x)).ToList();
                _cache.Set("products", products, TimeSpan.FromMinutes(30));

                return products;
            }
        }
    }
}
