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
        public string Brand { get; set; } // change data type from navpropert to string to avoid return the whole object nested
        public int BrandId { get; set; } // fk
        public string Category { get; set; } // change data type from navpropert to string to avoid return the whole object nested
        public int CategoryId { get; set; } // fk
    }
}