using RazorEX.BAL.DTOs.UserDTO;
using RazorEX.BAL.DTOs.UsersDTO;
using RazorEX.BAL.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.Contracts
{
    public interface IUser
    {
        OperationResult EditUser(EditUserDTO command);
        OperationResult EditUserFromUserPanel(EditUserDTO command);
        OperationResult Delete(int Id);
        UserDTO GetUserById(int userId);
        UserDTO GetUserByUserName(string UserName);
        UserFilterDTO GetUsersByFilter(int pageId, int take, string UserName);
        bool IsUserNameExist(string Username);
    }
}
