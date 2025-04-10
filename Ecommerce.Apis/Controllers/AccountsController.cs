using Ecommerce.Apis.DTOs;
using Ecommerce.Apis.Errors;
using Ecommerce.Core.Entites.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Services;

namespace Ecommerce.Apis.Controllers
{
    public class AccountsController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountsController( UserManager<AppUser> userManager, SignInManager<AppUser> signInManager )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region Register

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register( RegisterDto model )
        {
            var user = new AppUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                PhoneNumber = model.PhoneNumber
            };
            var Result = await _userManager.CreateAsync(user, model.Password);

            if ( !Result.Succeeded ) return BadRequest(new ApiResponse(400));
            var ReturnedUser = new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = "Will Be Done"
            };
            return Ok(ReturnedUser);
        }

        #endregion Register

        #region Login

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> LoginDto( LoginDto model )
        {
            var User = await _userManager.FindByEmailAsync(model.Email);
            if ( User is null ) return Unauthorized(new ApiResponse(401));      // if user not found
            var Result = await _signInManager.CheckPasswordSignInAsync(User, model.Passwrod, false);
            if ( !Result.Succeeded ) return Unauthorized(new ApiResponse(401)); // if password is wrong

            var ReturnedUser = new UserDto()
            {
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = "Will Be Done"
            };
            return Ok(ReturnedUser);
        }
    }

    #endregion Login
}