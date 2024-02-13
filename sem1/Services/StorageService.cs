using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using sem1.Dto;
using sem1.Models;
using sem1.Abstraction;

namespace sem1.Services
{
    public class StorageService : IStorageService
    {
        private readonly ProductStorageDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public StorageService(ProductStorageDbContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }

        public int AddStorage(StorageDto storage)
        {
            using (_context)
            {
                var entity = _mapper.Map<Storage>(storage);

                _context.ProductStorages.Add(entity);
                _context.SaveChanges();
                _cache.Remove("storages");

                return entity.Id;
            }
        }

        public IEnumerable<StorageDto> GetStorage()
        {
            using (_context)
            {
                if (_cache.TryGetValue("storages", out List<StorageDto> storages))
                    return storages;

                storages = _context.ProductStorages.Select(x => _mapper.Map<StorageDto>(x)).ToList();
                _cache.Set("storages", storages, TimeSpan.FromMinutes(30));

                return storages;
            }
        }
    }
}
