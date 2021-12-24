using RazorEx.DAL.Context;
using RazorEx.DAL.Entities;
using RazorEX.BAL.Contracts;
using RazorEX.BAL.DTOs;
using RazorEX.BAL.DTOs.UsersDTO;
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

        UserDTO ISIgnIn.SignIn(User signInDTO)
        {
            var user = _rXContext.Users
                    .FirstOrDefault(u => u.UserName == signInDTO.UserName && u.Password == signInDTO.Password);

            if (user == null)
                return null;

            var userDto = new UserDTO()
            {
                IsActive = user.IsActive,
                Password = user.Password,
                UserName = user.UserName,
                Role = user.Role,
                RegisterDate = user.CreationDate,
                UserId = user.Id,
            };
            return userDto;
        }
    }
}
