using Ecommerce.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Specification.ProductSpecifications
{
    public class ProductFilterationForCountSpec : BaseSpecifications<Product>
    {
        public ProductFilterationForCountSpec( ProductSpecParams specParams )
            : base(P =>
                ( string.IsNullOrEmpty(specParams.Search) || P.Name.ToLower().Contains(specParams.Search) )
                &&
                ( !specParams.BrandId.HasValue || P.BrandId == specParams.BrandId )
                &&
                ( !specParams.CategoryId.HasValue || P.CategoryId == specParams.CategoryId )
                )
        {
        }
    }
}