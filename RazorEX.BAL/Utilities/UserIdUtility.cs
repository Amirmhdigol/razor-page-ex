using RazorEx.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.Utilities
{
    public static class UserIdUtility
    {
        public static int Getid(this ClaimsPrincipal claims)
        {
            if (claims == null)
                throw new ArgumentNullException(nameof(claims));

            return Convert.ToInt32(claims.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
    }
}
