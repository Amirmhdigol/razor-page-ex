using RazorEx.DAL.Entities;
using RazorEX.BAL.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.Utilities.Mapper
{
    public static class ProductEpisodetoDTO
    {
        public static ProductEpisodesDTO Map(ProductEpisodes productEpisodes)
        {
            return new ProductEpisodesDTO()
            {
                EpisodeTitle = productEpisodes.EpisodeTitle,
                EpisodeFileName = productEpisodes.EpisodeFileName,
                EpisodeId = productEpisodes.EpisodeId,
                IsFree = productEpisodes.IsFree,
                Length = productEpisodes.Length,
                ProductId = productEpisodes.ProductId,
            };
        }
    }
}
