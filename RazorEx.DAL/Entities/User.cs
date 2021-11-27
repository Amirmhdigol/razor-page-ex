using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEx.DAL.Entities
{
    public class User : BaseEntities
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }

        public UserRole Role { get; set; }

        public ICollection<Post> Posts { get; set; }


        public enum UserRole
        {
            Admin,
            User,
            Writer
        }

    }
}
