using AutoMapper;
using Ecommerce.Apis.DTOs;
using Ecommerce.Core.Entites;

namespace Ecommerce.Apis.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(d=>d.Brand , o=>o.MapFrom(s=>s.Brand.Name))
                .ForMember(d=>d.Category , o=>o.MapFrom(s=>s.Category.Name));
            // cuz we need to take the name only of the brand or category so we need to configure it like that




        }




    }
}
