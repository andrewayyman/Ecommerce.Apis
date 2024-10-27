using AutoMapper;
using Ecommerce.Apis.DTOs;
using Ecommerce.Core.Entites;
using Ecommerce.Core.Repository.Contract;
using Ecommerce.Core.Specification.ProductSpecifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Apis.Controllers
{


    public class ProductController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IMapper _mapper;

        public ProductController( IGenericRepository<Product> productRepo , IMapper mapper )
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }


        // BaseUrl/api/Product
        #region GetProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var spec = new ProductWithBrandAndCategorySpecifications();

            var products = await _productRepo.GetAllWithSpecAsync(spec);

            var mappedProducts = _mapper.Map <IEnumerable<Product> , IEnumerable<ProductDto> >(products);

            return Ok(mappedProducts);
        }

        #endregion



        // BaseUrl/api/Product
        #region GetProductsById
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductById( int id )
        {
            var spec = new ProductWithBrandAndCategorySpecifications(id);

            var product = await _productRepo.GetWithSpecAsync(spec);
            if ( product == null )
            {
                return NotFound(new { Message = " Not Found", StatusCode = 404 }); // 404
            }

            var mappedProduct = _mapper.Map<Product, ProductDto> (product);

            return Ok(mappedProduct);

        }

        #endregion



    }
}
