using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMapper
    {
        public static CommentDTO ToCommentDto(this Comment commentModel)
        {
            return new CommentDTO
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                StockId = commentModel.StockId

            };
        }

        public static Comment ToCommentFromCreateDto(this CreateCommentDto commentDto, int stockId)
        {
            return new Comment
            {
                
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = stockId

            };
        }

        public static Comment ToCommentFromUpdateDto(this CommentUpdateDto commentDto)
        {
            return new Comment
            {
                
                Title = commentDto.Title,
                Content = commentDto.Content,
                
            };
        }
    }
}