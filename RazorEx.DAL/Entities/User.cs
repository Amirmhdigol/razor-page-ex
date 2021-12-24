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
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string RePassword { get; set; } 
        public UserRole Role { get; set; }
        public string ActiveCode { get; set; }
        public bool IsActive { get; set; }

        #region Relations
        public ICollection<Post> Posts { get; set; }
        public ICollection<PostComment> PostComments { get; set; }
        #endregion
    }

    public enum UserRole
    {
        Admin,
        User,
        Writer
    }
}
