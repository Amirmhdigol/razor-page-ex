﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEx.DAL.Entities
{
    public class User:BaseEntity
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string RePassword { get; set; } 
        public UserRole Role { get; set; }
       
        #region Relations

        public ICollection<Post> Posts { get; set; }

        #endregion
    }

    public enum UserRole
    {
        Admin,
        User,
        Writer
    }
}
