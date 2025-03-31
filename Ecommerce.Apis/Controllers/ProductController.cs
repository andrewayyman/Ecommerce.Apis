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
        private readonly IGenericRepository<ProductBrand> _brandRepo;
        private readonly IGenericRepository<ProductCategory> _categoryRepo;
        private readonly IMapper _mapper;

        public ProductController(
            IGenericRepository<Product> productRepo,
            IGenericRepository<ProductBrand> brandRepo,
            IGenericRepository<ProductCategory> categoryRepo,
            IMapper mapper

            )
        {
            _productRepo = productRepo;
            _mapper = mapper;
            _brandRepo = brandRepo;
            _categoryRepo = categoryRepo;
        }

        // BaseUrl/api/Product

        #region GetProducts

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<ProductDto>), StatusCodes.Status200OK)] // improve swagger doc
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetProducts()
        {
            var spec = new ProductWithBrandAndCategorySpecifications();
            var products = await _productRepo.GetAllWithSpecAsync(spec);
            var mappedProducts = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);
            return Ok(mappedProducts);
        }

        #endregion GetProducts

        // BaseUrl/api/Product

        #region GetProductsById

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)] // improve swagger doc
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProductById( int id )
        {
            var spec = new ProductWithBrandAndCategorySpecifications(id);
            var product = await _productRepo.GetWithSpecAsync(spec);
            if ( product is null ) return NotFound(new ApiResponse(404)); // NotFound404
            var mappedProduct = _mapper.Map<Product, ProductDto>(product);
            return Ok(mappedProduct);
        }

        #endregion GetProductsById

        // BaseUrl/api/Product/brands

        #region GetAllBrands

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrand()
        {
            var brands = await _brandRepo.GetAllAsync();
            return Ok(brands);
        }

        #endregion GetAllBrands

        // BaseUrl/api/Product/categories

        #region GetAllCategories

        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetAllCategory()
        {
            var categories = await _categoryRepo.GetAllAsync();
            return Ok(categories);
        }

        #endregion GetAllCategories
    }
}