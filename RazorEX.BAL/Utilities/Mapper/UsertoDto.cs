using RazorEx.DAL.Entities;
using RazorEX.BAL.DTOs.UserDTO;
using RazorEX.BAL.DTOs.UsersDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.Utilities.Mapper
{
    public static class UsertoDto
    {
        public static UserDTO Map(this User user)
        {
            return new UserDTO()
            {
               UserName = user.UserName,
               Password = user.Password,
               RegisterDate = user.CreationDate,
               Role = user.Role,
               UserId = user.Id
            };
        }
    }
}
