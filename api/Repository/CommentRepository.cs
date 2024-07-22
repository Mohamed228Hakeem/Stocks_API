using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Comment;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDBContext _context;

        public CommentRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.comments.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Comment> CreateAsync (Comment commentModel)
        {
            await _context.comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;

        }

        public async Task<Comment?> UpdateAsync(int id, Comment commentModel)
        {
            var ExistingComment = await _context.comments.FindAsync(id);
            if (ExistingComment == null)
            {
                return null;
            }
            ExistingComment.Content = commentModel.Content;
            ExistingComment.Title = commentModel.Title;
            await _context.SaveChangesAsync();
            return ExistingComment;
        }

        public async Task<Comment> DeleteAsync(int id)
        {
            var commentModel = await _context.comments.FirstOrDefaultAsync(x => x.Id == id);
            if (commentModel == null){
                return null;
            }
            _context.Remove(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }
    }
}