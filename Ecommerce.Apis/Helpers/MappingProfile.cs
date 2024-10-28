using AutoMapper;
using Ecommerce.Apis.DTOs;
using Ecommerce.Core.Entites;

namespace Ecommerce.Apis.Helpers
{
    public class MappingProfile : Profile
    {
        //private readonly IConfiguration _configuration;


        public MappingProfile( )
        {

            // cuz we need to take the name only of the brand or category so we need to configure it like that
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand.Name))
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());

            ///Why Not MapFrom(_configuration["ApiBaseUrl"]}/{source.PictureUrl}) ??
            /// if we pass this line inside map from will throw exception
            /// cuz in program file we use ctor with no params so we cannot inject IConfiguration in this class
            /// we will create UrlResolver class and inject it and resolve it there

        }




    }
}
