using RazorEx.DAL.Entities;
using RazorEX.BAL.DTOs;
using RazorEX.BAL.DTOs.UsersDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.Contracts
{
    public interface ISIgnIn
    {
        public UserDTO SignIn(User signInDTO);
    }
}
