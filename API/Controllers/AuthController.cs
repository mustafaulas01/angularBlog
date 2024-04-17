using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public AuthController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            
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