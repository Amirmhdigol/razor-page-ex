using RazorEX.BAL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.Contracts
{
    public interface ISIgnIn
    {
        public UserDTO SignIn(SignInDTO signInDTO);
    }
}
