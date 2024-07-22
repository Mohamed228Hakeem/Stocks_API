using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Comment
{
    public class CommentUpdateDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Content must be at least 5 characters")]
        [MaxLength(255, ErrorMessage = "Content must be less than 255 characters")]
        public string Content { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Title must be at least 5 characters")]
        [MaxLength(80, ErrorMessage = "Title must be less than 80 characters")]
        public string Title { get; set; } = string.Empty;
    }
}