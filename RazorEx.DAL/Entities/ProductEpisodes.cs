using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEx.DAL.Entities
{
    public class ProductEpisodes
    {
        public TimeSpan Length { get; set; }
        [Key]
        public int EpisodeId { get; set; }
        public int ProductId { get; set; }
        [Required]
        public string EpisodeTitle { get; set; }
        [Required]
        public string EpisodeFileName { get; set; }
        public bool IsFree { get; set; }


        [ForeignKey("ProductId")]
        public Products Products { get; set; }
    }
}
