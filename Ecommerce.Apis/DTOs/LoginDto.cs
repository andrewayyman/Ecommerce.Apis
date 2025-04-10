using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Apis.DTOs
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Passwrod { get; set; }

    }
}
