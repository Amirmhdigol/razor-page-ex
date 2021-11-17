using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.DTOs
{
    public class SignupDTO
    {
        public string UserName { get; set; }
        public int Password { get; set; }
        public int RePassword { get; set; }
    }
}
