﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.DTOs.ProductCommentDTOs
{
    public class CreateProductCommentDTO
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string Text { get; set; }

    }
}
