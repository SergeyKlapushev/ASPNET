using AutoMapper;
using sem1.Models;
using sem1.Dto;
using System.Text.RegularExpressions;

namespace sem1.Repo
{
    public class MappingProFile : Profile
    {
        public MappingProFile() 
        {
            CreateMap<Product, ProductDto>(MemberList.Destination).ReverseMap();
            CreateMap<Category, CategoryDto>(MemberList.Destination).ReverseMap();
            CreateMap<Storage, StorageDto>(MemberList.Destination).ReverseMap();
        }
    }
}
