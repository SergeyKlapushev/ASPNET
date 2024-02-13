using Autofac;
using AutoMapper;
using sem1.Dto;
using sem1.Models;

namespace sem1.Mapper
{
    public class MapperProFile : Profile
    {
        public MapperProFile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Storage, StorageDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
