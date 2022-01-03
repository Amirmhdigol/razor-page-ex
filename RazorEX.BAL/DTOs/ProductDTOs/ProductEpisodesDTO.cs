using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.DTOs.ProductDTOs
{
    public class ProductEpisodesDTO
    {
        public TimeSpan Length { get; set; }
        public int EpisodeId { get; set; }
        public int ProductId { get; set; }
        public string EpisodeTitle { get; set; }
        public string EpisodeFileName { get; set; }
        public bool IsFree { get; set; }
    }
}
