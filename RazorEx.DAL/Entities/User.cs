﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEx.DAL.Entities
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public int Password { get; set; }
        public int RePassword { get; set; }
    }
}