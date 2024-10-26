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
        public ProductWithBrandAndCategorySpecifications() : base()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);
            
        }

        // we make this ctor to sent the id for the where clause by chaining on the parameterize ctor in base which take expression parameter
        public ProductWithBrandAndCategorySpecifications(int id):base(p=>p.Id == id)
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);

        }
         


    }
}
