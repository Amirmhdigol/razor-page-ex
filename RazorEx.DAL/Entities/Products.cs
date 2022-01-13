using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEx.DAL.Entities
{
    public class Products : BaseEntity
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Price { get; set; }

        public int? EpisodeId { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; }

        public string Tags { get; set; }
        public int Visit { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ProductImageName { get; set; }

        public string DemoFileName { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int TeacherId { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CategoryId { get; set; }

        public int? SubCategoryId { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int LevelId { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int StatusId { get; set; }

        #region Relations

        [ForeignKey("TeacherId")]
        public User Teacher { get; set; }

        [ForeignKey("CategoryId")]
        public Category MainCategory { get; set; }

        [ForeignKey("SubCategoryId")]
        public Category SubCategory { get; set; }
        public ICollection<ProductEpisodes> ProductEpisodes { get; set; }
        public ProductStatus ProductStatus { get; set; }
        public ProductLevel ProductLevel { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<UserProducts> UserProducts { get; set; }
        public ICollection<ProductVote> ProductVotes { get; set; }
        #endregion

    }
}
