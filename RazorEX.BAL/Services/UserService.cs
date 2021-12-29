using Microsoft.EntityFrameworkCore;
using RazorEx.DAL.Context;
using RazorEx.DAL.Entities;
using RazorEX.BAL.Contracts;
using RazorEX.BAL.DTOs.UserDTO;
using RazorEX.BAL.DTOs.UsersDTO;
using RazorEX.BAL.DTOs.Wallet;
using RazorEX.BAL.Utilities;
using RazorEX.BAL.Utilities.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RazorEX.BAL.Services
{
    public class UserService : IUser
    {
        private readonly RXContext _context;

        public UserService(RXContext context)
        {
            _context = context;
        }

        public OperationResult Delete(int Id)
        {
            var FindedUser = _context.Users.Find(Id);

            if (FindedUser == null)
                return null;

            FindedUser.IsDelete = true;
            _context.SaveChanges();
            return OperationResult.Success();
        }

        public bool IsUserNameExist(string Username)
        {
            return _context.Users.Any(p => p.UserName == Username);
        }

        public OperationResult EditUser(EditUserDTO command)
        {
            var FindedUser = _context.Users.FirstOrDefault(a => a.Id == command.UserId);

            var newUserName = command.FullName;

            if (FindedUser == null)
                return OperationResult.NotFound();

            if (FindedUser.UserName != newUserName)
                if (IsUserNameExist(newUserName))
                    return OperationResult.Error("Username تکراری است");

            FindedUser.UserName = command.FullName;
            FindedUser.Role = command.Role;
            FindedUser.Id = command.UserId;

            try
            {
                _context.Users.Update(FindedUser);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return OperationResult.Error();
            }
            return OperationResult.Success();
        }

        public UserDTO GetUserById(int userId)
        {
            var FindedUser = _context.Users.Find(userId);

            if (FindedUser == null)
                return null;

            return new UserDTO()
            {
                UserName = FindedUser.UserName,
                Password = FindedUser.Password,
                RegisterDate = FindedUser.CreationDate,
                Role = FindedUser.Role,
                UserId = FindedUser.Id
            };
        }

        public UserFilterDTO GetUsersByFilter(int pageId, int take, string UserName)
        {
            var result = _context.Users
                .Include(a => a.Posts)
                .Include(a => a.PostComments)
                .OrderByDescending(a => a.CreationDate)
                .Where(a => !a.IsDelete)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(UserName))
                result = _context.Users
                    .Where(a => a.UserName.Contains(UserName));

            var skip = (pageId - 1) * take;
            var model = new UserFilterDTO()
            {
                Users = result.Skip(skip).Take(take).Select(user => new UserDTO()
                {
                    Password = user.Password,
                    Role = user.Role,
                    UserName = user.UserName,
                    RegisterDate = user.CreationDate,
                    UserId = user.Id,
                }).ToList()
            };
            model.GeneratePaging(result, take, pageId);
            return model;
        }

        public UserDTO GetUserByUserName(string UserName)
        {
            var FindedUser = _context.Users.FirstOrDefault(a => a.UserName == UserName);

            if (FindedUser == null)
                return null;

            var MaptoDTO = new UserDTO()
            {
                UserName = FindedUser.UserName,
                UserId = FindedUser.Id,
                RegisterDate = FindedUser.CreationDate,
                Role = FindedUser.Role
            };
            return MaptoDTO;
        }

        public OperationResult EditUserFromUserPanel(EditUserDTO command)
        {
            var FindedUser = _context.Users.FirstOrDefault(a => a.Id == command.UserId);
            var newUserName = command.FullName;

            if (FindedUser == null)
                return OperationResult.NotFound();

            if (FindedUser.UserName != newUserName)
                if (IsUserNameExist(newUserName))
                    return OperationResult.Error("Username تکراری است");

            FindedUser.UserName = command.FullName;
            //FindedUser.Password = command.Password;

            _context.Users.Update(FindedUser);
            _context.SaveChanges();
            return OperationResult.Success();
        }

        public bool CheckOldPassExist(string UserName, string oldpassword)
        {
            return _context.Users.Any(a => a.UserName == UserName && a.Password == oldpassword);
        }
        public OperationResult EditPassUserpanel(EditPassDTO command)
        {
            var Checkoldpass = CheckOldPassExist(command.UserName, command.OldPassword);

            if (!Checkoldpass)
                return OperationResult.Error("رمز عبور اشتباه است");

            var user = _context.Users.FirstOrDefault(a => a.UserName == command.UserName);

            if (user != null)
                user.Password = command.Password;
            else
                return OperationResult.NotFound();

            _context.Users.Update(user);
            _context.SaveChanges();

            return OperationResult.Success();
        }

        public int GetUserIdByUserName(string UserName)
        {
            return _context.Users.First(a => a.UserName == UserName).Id;
        }

        public int UserWalletBalance(string UserName)
        {
            var FindedUserId = GetUserIdByUserName(UserName);

            var Deposit = _context.Wallets.Include(a => a.User)
                .Where(a => a.UserId == FindedUserId && a.TypeId == 1 && a.IsPay)
                .Select(a => a.Amount).ToList();

            var Harvest = _context.Wallets.Include(a => a.User)
                .Where(a => a.UserId == FindedUserId && a.TypeId == 2)
                .Select(a => a.Amount).ToList();

            return Deposit.Sum() - Harvest.Sum();
        }
        public List<UserWalletDTO> UserTransactionList(string UserName)
        {
            var FindedUserId = GetUserIdByUserName(UserName);

            var UserWallets = _context.Wallets.Include(a => a.User)
                .Where(a => a.IsPay && a.UserId == FindedUserId)
                .Select(a => new UserWalletDTO()
                {
                    Amount = a.Amount,
                    Description = a.Description,
                    Type = a.TypeId,
                    TransactionDate = a.CreationDate
                }).ToList();

            return UserWallets;
        }

        public int ChargeWallet(string UserName, int ChargeAmount, string Description, bool IsPay = false)
        {
            WalletDTO wallet = new()
            {
                Amount = ChargeAmount,
                CreationDate = DateTime.Now,
                Description = Description,
                IsPay = IsPay,
                TypeId = 1,
                UserId = GetUserIdByUserName(UserName)
            };
            return AddTransaction(wallet);
        }

        public int AddTransaction(WalletDTO walletDTO)
        {
            Wallet MappedWallet = new()
            {
                Amount = walletDTO.Amount,
                CreationDate = walletDTO.CreationDate,
                Description = walletDTO.Description,
                IsPay = walletDTO.IsPay,
                TypeId = walletDTO.TypeId,
                UserId = walletDTO.UserId,
            };

            _context.Wallets.Add(MappedWallet);
            _context.SaveChanges();
            return MappedWallet.Id;
        }

        public WalletDTO GetWalletByWalletId(int id)
        {
            var FindedWallet = _context.Wallets.Find(id);
            var MappedWallet = new WalletDTO()
            {
                Amount = FindedWallet.Amount,
                CreationDate = FindedWallet.CreationDate,
                Description = FindedWallet.Description,
                IsPay = FindedWallet.IsPay,
                TypeId = FindedWallet.TypeId,
                UserId = FindedWallet.UserId,
            };
            return MappedWallet;
        }
        public OperationResult UpdateWallet(WalletDTO walletDTO)
        {
            var MappedWallet = new Wallet()
            {
                Amount = walletDTO.Amount,
                CreationDate = walletDTO.CreationDate,
                Description = walletDTO.Description,
                IsPay = walletDTO.IsPay,
                TypeId = walletDTO.TypeId,
                UserId = walletDTO.UserId,
            };
            _context.Wallets.Update(MappedWallet);
            _context.SaveChanges();
            return OperationResult.Success();
        }

        public List<UserDTO> DeletedUsers()
        {
            var model = _context.Users
                .IgnoreQueryFilters()
                .OrderByDescending(a => a.CreationDate)
                .Where(u => u.IsDelete == true)
                .Select(u => new UserDTO()
                {
                    UserId = u.Id,
                    UserName = u.UserName,
                    Password = u.Password,
                    RegisterDate = u.CreationDate,
                    Role = u.Role
                }).ToList();

            return model;
        }
    }
}