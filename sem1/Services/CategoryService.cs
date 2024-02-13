using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using sem1.Abstraction;
using sem1.Dto;
using sem1.Models;
using System;

namespace sem1.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ProductStorageDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public CategoryService(ProductStorageDbContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }
        public int AddCategory(CategoryDto category)
        {
            using (_context)
            {
                var entity = _mapper.Map<Category>(category);

                _context.Categories.Add(entity);
                _context.SaveChanges();
                _cache.Remove("categories");

                return entity.Id;
            }
        }

        public IEnumerable<CategoryDto> GetCategories()
        {
            using (_context)
            {
                if (_cache.TryGetValue("categories", out List<CategoryDto> categories))
                    return categories;

                categories = _context.Categories.Select(x => _mapper.Map<CategoryDto>(x)).ToList();
                _cache.Set("categories", categories, TimeSpan.FromMinutes(30));

                return categories;
            }
        }
    }
}
