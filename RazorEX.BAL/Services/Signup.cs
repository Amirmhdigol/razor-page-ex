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

namespace RazorEX.BAL.Services
{
    public  class Signup : ISignup
    {
        private readonly RXContext _rXContext;

        public Signup(RXContext rXContext)
        {
           _rXContext = rXContext;
        }

        public OperationResult Register(SignupDTO signupDTO)
        {
            var isUserNameExist = _rXContext.Users.Any(u => u.UserName == signupDTO.UserName);

            if (isUserNameExist)
                return OperationResult.Error("نام کاربری تکراری است");

            _rXContext.Users.Add(new User()
            {
                UserName = signupDTO.UserName,
                Password = signupDTO.Password,
                RePassword = signupDTO.RePassword
            });
            _rXContext.SaveChanges();
            return OperationResult.Success();
        }
    }
}
