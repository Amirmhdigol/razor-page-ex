using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEx.DAL.Entities
{
    public class User:BaseEntity
    {
        public User()
        {

        }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string RePassword { get; set; } 
        public UserRole Role { get; set; }
        public string ActiveCode { get; set; }
        public bool IsActive { get; set; }

        #region Relations
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<PostComment> PostComments { get; set; }
        public virtual ICollection<Wallet> Wallets { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<UserProducts> UserProducts { get; set; }
        public ICollection<UserDiscounts> UserDiscounts { get; set; }
        #endregion
    }

    public enum UserRole
    {
        Admin,
        User,
        Writer
    }
}
