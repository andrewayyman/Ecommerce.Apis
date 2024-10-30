using Ecommerce.Core.Entites;

namespace Ecommerce.Apis.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; }
        public int BrandId { get; set; } // fk 
        public string Category { get; set; }
        public int CategoryId { get; set; } // fk




    }
}
