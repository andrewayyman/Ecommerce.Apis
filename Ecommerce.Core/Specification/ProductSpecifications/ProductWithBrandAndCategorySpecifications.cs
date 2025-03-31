using Ecommerce.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Specification.ProductSpecifications
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product>
    {
        public ProductWithBrandAndCategorySpecifications( ProductSpecParams specParams )
            : base(P =>
                ( !specParams.BrandId.HasValue || P.BrandId == specParams.BrandId ) &&   // to check if the BrandId has value or not
                ( !specParams.CategoryId.HasValue || P.CategoryId == specParams.CategoryId )
                )
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);
            if ( !string.IsNullOrEmpty(specParams.Sort) )
            {
                switch ( specParams.Sort )
                {
                    case "PriceAsc":
                        AddOrderBy(p => p.Price);
                        break;

                    case "PriceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;

                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }

            // Skips a certain set of records, by the page number * page size.
            // Takes only the required amount of data, set by page size.
            ApplyPagination(specParams.PageSize * ( specParams.PageIndex - 1 ), specParams.PageSize);
        }

        // we make this ctor to sent the id for the where clause by chaining on the parameterize ctor in base which take expression parameter
        public ProductWithBrandAndCategorySpecifications( int id ) : base(p => p.Id == id)
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);
        }
    }
}