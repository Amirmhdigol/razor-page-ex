using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.DTOs.UserDTO
{
    public class UserFilterDTO
    {
        public List<UsersDTO.UserDTO> Users { get; set; }
        public string UserName { get; set; }
    }
}
