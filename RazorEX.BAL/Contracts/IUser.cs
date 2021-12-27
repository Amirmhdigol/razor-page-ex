using RazorEX.BAL.DTOs.UserDTO;
using RazorEX.BAL.DTOs.UsersDTO;
using RazorEX.BAL.DTOs.Wallet;
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
        OperationResult Delete(int Id);
        UserDTO GetUserById(int userId);
        UserDTO GetUserByUserName(string UserName);
        UserFilterDTO GetUsersByFilter(int pageId, int take, string UserName);
        bool IsUserNameExist(string Username);
        int GetUserIdByUserName(string UserName);

        #region UserPanel
        OperationResult EditPassUserpanel(EditPassDTO command);
        OperationResult EditUserFromUserPanel(EditUserDTO command);
        #endregion

        #region Wallet
        int UserWalletBalance(string UserName);
        int ChargeWallet(string UserName, int ChargeAmount, string Description, bool IsPay = false);
        List<UserWalletDTO> UserTransactionList(string UserName);
        int AddTransaction(WalletDTO walletDTO);
        WalletDTO GetWalletByWalletId(int id);
        OperationResult UpdateWallet(WalletDTO walletDTO);
        #endregion
    }
}
