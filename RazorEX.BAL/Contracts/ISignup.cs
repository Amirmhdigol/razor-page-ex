using RazorEX.BAL.DTOs;
using RazorEX.BAL.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.Contracts
{
    public interface ISignup
    {
          public OperationResult Register(SignupDTO signupDTO); 
    }
}
