using ECommerce.Entity.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Abstracts
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
