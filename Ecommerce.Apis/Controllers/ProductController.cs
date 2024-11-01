using AutoMapper;
using Ecommerce.Apis.DTOs;
using Ecommerce.Apis.Errors;
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
        private readonly IGenericRepository<ProductBrand> _brandRepo;
        private readonly IGenericRepository<ProductCategory> _categoryRepo;

        public ProductController( 
            IGenericRepository<Product> productRepo ,
            IMapper mapper,
            IGenericRepository<ProductBrand> brandRepo,
            IGenericRepository<ProductCategory> categoryRepo

            )
        {
            _productRepo = productRepo;
            _mapper = mapper;
            _brandRepo = brandRepo;
            _categoryRepo = categoryRepo;
        }


        // BaseUrl/api/Product
        #region GetProducts
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
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

        [ProducesResponseType(typeof(ProductDto) , StatusCodes.Status200OK )]
        [ProducesResponseType(typeof(ApiResponse) , StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductById( int id )
        {
            var spec = new ProductWithBrandAndCategorySpecifications(id);

            var product = await _productRepo.GetWithSpecAsync(spec);
            if ( product == null )
            {
                return NotFound(new ApiResponse(404) ); // 404
            }

            var mappedProduct = _mapper.Map<Product, ProductDto> (product);

            return Ok(mappedProduct);

        }

        #endregion


        // BaseUrl/api/Product/brands
        #region GetAllBrands

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetAllBrand ()
        {
            var brands = await _brandRepo.GetAllAsync();
            return Ok(brands);
        }

        #endregion


        // BaseUrl/api/Product/categories  
        #region GetAllCategories

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetAllCategory ()
        {
            var categories = await _categoryRepo.GetAllAsync();
            return Ok(categories);
        }

        #endregion


    }
}
