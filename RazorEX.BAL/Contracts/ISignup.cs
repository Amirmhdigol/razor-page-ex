using RazorEx.DAL.Entities;
using RazorEX.BAL.DTOs;
using RazorEX.BAL.DTOs.UsersDTO;
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
          public int Register(User user);
          public bool ActiveAccount(string activeCode);
    }
}
