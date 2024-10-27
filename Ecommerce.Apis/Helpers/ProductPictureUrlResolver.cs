using AutoMapper;
using AutoMapper.Execution;
using Ecommerce.Apis.DTOs;
using Ecommerce.Core.Entites;

namespace Ecommerce.Apis.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolver( IConfiguration configuration )
        {
            _configuration = configuration;
        }

        public string Resolve( Product source, ProductDto destination, string destMember, ResolutionContext context )
        {
            if( ! string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_configuration["ApiBaseUrl"]}/{source.PictureUrl}";
            }
            return String.Empty;
            
        }


    }
}
