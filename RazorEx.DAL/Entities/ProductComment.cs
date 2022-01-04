using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEx.DAL.Entities
{
    public class ProductComment : BaseEntity
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        [Required]
        public string Text { get; set; }

        #region Relations
        public User User { get; set; }
        public Products Product { get; set; }
        #endregion
    }
}
