using Ecommerce.Core.Entites.Identity;
using Ecommerce.Repository.Identity;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Apis.Extensions
{
    public static class IdentityServicesExtension
    {
        public static IServiceCollection AddIdentityServices( this IServiceCollection Services )
        {
            Services.AddIdentity<AppUser, IdentityRole>() // can control options of password
                          .AddEntityFrameworkStores<AppIdentityDbContext>();

            Services.AddAuthentication(); // allow dependency injection for authentication [usermanager , signinmanager , rolemanager]

            return Services;
        }
    }
}