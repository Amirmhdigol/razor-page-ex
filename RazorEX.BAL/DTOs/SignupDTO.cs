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
        public string Password { get; set; }
        public string RePassword { get; set; }
    }
}
