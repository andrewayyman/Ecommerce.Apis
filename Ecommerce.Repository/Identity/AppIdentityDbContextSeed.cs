using Ecommerce.Core.Entites.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync( UserManager<AppUser> userManager )
        {
            if ( !userManager.Users.Any() )
            {
                var user = new AppUser()
                {
                    DisplayName = "Andrew Ayman",
                    Email = "Andrewayman1000@gmail.com",
                    UserName = "andrewayman1000",
                    PhoneNumber = "01206741192"
                };
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}