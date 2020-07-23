using Insta.DataAccess.Interfaces;
using Insta.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insta.DataAccess.Implementations
{
    public class CommentsRepository : IRepository<Comment>
    {
        private readonly DataContext _context;

        public CommentsRepository(DataContext context)
        {
            _context = context;
        }

        public Task<Comment> GetAsync(Guid id)
        {
            return _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task<List<Comment>> GetAllByPostIdAsync(Guid id)
        {
            return _context.Comments.Where(c => c.PostId == id).ToListAsync();
        }

        public Task<List<Comment>> GetAllAsync()
        {
            return _context.Comments.ToListAsync();
        }

        public ValueTask<EntityEntry<Comment>> CreateAsync(Comment entity)
        {
            var createdComment = _context.Comments.AddAsync(entity);
            return createdComment;
        }

        public ValueTask<Comment> EditAsync(Comment entity)
        {
            var comment = _context.Comments.FindAsync(entity.Id);
            if (comment != null)
            {
                comment.Result.Text = entity.Text;
            }
            return comment;
        }

        public void Remove(Comment entity)
        {
            _context.Comments.Remove(entity);
        }

        public bool Exist(Guid id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }

        public bool isExist(Guid postId, string userId)
        {
            return _context.Comments.Any(x => x.PostId == postId && x.Post.UserId == userId);
        }
    }
}
