using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace API.BusinessLayer.Abstract
{
    public interface ITokenService
    {
        string CreateJwtToken(IdentityUser USER,List<string>roles);
    }
}