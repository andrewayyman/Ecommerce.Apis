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

        public ProductController( IGenericRepository<Product> productRepo )
        {
            _productRepo = productRepo;
        }


        // BaseUrl/api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var spec = new ProductWithBrandAndCategorySpecifications();
            var products = await _productRepo.GetAllWithSpecAsync(spec);
            return Ok(products);
        }




        // BaseUrl/api/Product
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductById( int id )
        {
            var spec = new ProductWithBrandAndCategorySpecifications(id);

            var product = await _productRepo.GetWithSpecAsync(spec);
            if ( product == null )
            {
                return NotFound(new { Message = " Not Found", StatusCode = 404 }); // 404
            }
            return Ok(product);

        }




    }
}
