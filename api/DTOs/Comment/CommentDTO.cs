using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.DTOs.Comment
{
    public class CommentDTO
    {
            public int? StockId { get; set; }

            public int Id { get; set; }

            public string Content { get; set; } = string.Empty;
            public string Title { get; set; } = string.Empty;

            public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}