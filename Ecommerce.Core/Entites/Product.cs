using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Entites
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string PictureUrl { get; set; }

        public decimal Price { get; set; }

        // nav prop
        public ProductBrand Brand { get; set; }
        public int BrandId { get; set; } // fk 

        // nav prop 
        public ProductCategory Category { get; set; }
        public int CategoryId { get; set; } // fk
    }
}
