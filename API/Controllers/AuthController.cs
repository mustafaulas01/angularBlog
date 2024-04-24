using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.BusinessLayer.Abstract;
using API.BusinessLayer.Concrete;
using API.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        
        private readonly UserManager<IdentityUser> _userManager;

        private readonly ITokenService _tokenService;
        public AuthController(UserManager<IdentityUser> userManager,ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;   
            
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            //check email

            var identiyUser = await _userManager.FindByEmailAsync(model.Email);
            //check password
            if (identiyUser is not null)
            {

                var checkPasswordResult = await _userManager.CheckPasswordAsync(identiyUser, model.Password);
                if (checkPasswordResult)
                {
                    //create token
                   
                   var roles= await _userManager.GetRolesAsync(identiyUser);
                      
                   var token= _tokenService.CreateJwtToken(identiyUser,roles.ToList());
                    var response=new LoginResponseDto()
                    {
                        Email=model.Email,
                        Roles=roles.ToList(),
                        Token=token

                    };
                    return Ok(response);
                }

            }
           ModelState.AddModelError("", "Email or Password Incorrect");
           return ValidationProblem(ModelState);

        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register( [FromBody] RegisterDto model )

        {
            //create IdentyUser
            var user =new IdentityUser
            {
                UserName = model.Email.Trim(),
                Email=model.Email.Trim()

            };

            var  IdentityResult= await _userManager.CreateAsync(user,model.Password);

            if(IdentityResult.Succeeded)
            {
               var identityRoleResult=  await _userManager.AddToRoleAsync(user,"Reader");
               return Ok();

            }
            else
            {
             if(IdentityResult.Errors.Any())
             {
                foreach (var error in IdentityResult.Errors)    
                {
                    ModelState.AddModelError("",error.Description);
                }
             }
             
            }
 
            
            return ValidationProblem(ModelState);
        }
    }
}