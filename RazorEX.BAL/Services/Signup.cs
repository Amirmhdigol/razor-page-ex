using RazorEX.BAL.Contracts;
using RazorEX.BAL.DTOs;
using Microsoft.EntityFrameworkCore;
using RazorEX.BAL.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RazorEx.DAL;
using RazorEx.DAL.Context;
using RazorEx.DAL.Entities;
using static RazorEx.DAL.Entities.User;
using RazorEX.BAL.DTOs.UsersDTO;

namespace RazorEX.BAL.Services
{
    public  class Signup : ISignup
    {
        private readonly RXContext _rXContext;

        public Signup(RXContext rXContext)
        {
           _rXContext = rXContext;
        }

        public bool ActiveAccount(string activeCode)
        {
            var user = _rXContext.Users.SingleOrDefault(u => u.ActiveCode == activeCode);
            if (user == null || user.IsActive)
                return false;

            user.IsActive = true;
            user.ActiveCode = NameGenerator.GenerateUniqCode();
            _rXContext.SaveChanges();

            return true;
        }

        public int Register(User user)
        {
            var isUserNameExist = _rXContext.Users.Any(u => u.UserName == user.UserName);

            if (isUserNameExist)
                throw new Exception();

            //var AddedUser = _rXContext.Users.Add(new User()
            //{
            //    UserName = user.UserName,
            //    Password = user.Password,
            //    RePassword = user.RePassword,
            //   IsDelete = false,
            //    CreationDate = DateTime.Now,
            //    IsActive = false,
            //    Role = UserRole.User,
            //    ActiveCode = NameGenerator.GenerateUniqCode()
            //});
            _rXContext.Users.Add(user);
            _rXContext.SaveChanges();
            return user.Id;
        }
    }
}
