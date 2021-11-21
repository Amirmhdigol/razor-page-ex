using RazorEx.DAL.Context;
using RazorEX.BAL.Contracts;
using RazorEX.BAL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.Services
{
    public class SignIn : ISIgnIn
    {
        private readonly RXContext _rXContext;

        public SignIn(RXContext rXContext)
        {
            _rXContext = rXContext;
        }

        UserDTO ISIgnIn.SignIn(SignInDTO signInDTO)
        {
            var user = _rXContext.Users
     .FirstOrDefault(u => u.UserName == signInDTO.UserName && u.Password == signInDTO.Password);

            if (user == null)
                return null;

            var userDto = new UserDTO()
            {
                Password = user.Password,
                UserName = user.UserName,
            };
            return userDto;

        }
    }
}
