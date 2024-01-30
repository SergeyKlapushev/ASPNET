using AutoMapper;
using sem1.Models;

namespace sem1.Repo
{
    public class MappingProFile : Profile
    {
        public MappingProFile() 
        {
            CreateMap<Product, ProductDTO>(MemberList.Destination).ReverseMap();
        }
    }
}
