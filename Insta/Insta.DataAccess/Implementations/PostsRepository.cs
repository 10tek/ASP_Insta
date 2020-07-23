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
    public class PostsRepository : IRepository<Post>
    {
        private readonly DataContext _context;

        public PostsRepository(DataContext context)
        {
            _context = context;
        }

        public Task<Post> GetAsync(Guid id)
        {
            return _context.Posts.Include(p => p.Likes).FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task<List<Post>> GetAllAsync()
        {
            return _context.Posts.Include(c => c.Comments).Include(u => u.User).Include(l => l.Likes).ToListAsync();
        }

        public ValueTask<EntityEntry<Post>> CreateAsync(Post entity)
        {
            var createdPost = _context.Posts.AddAsync(entity);
            return createdPost;
        }

        public ValueTask<Post> EditAsync(Post entity)
        {
            var post = _context.Posts.FindAsync(entity.Id);
            if (post != null)
            {
                post.Result.PhotoPath = entity.PhotoPath;
            }
            return post;
        }

        public Task<List<Post>> GetAllByPostIdAsync(Guid id)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(Post entity)
        {
            _context.Posts.Remove(entity);
        }

        public bool Exist(Guid id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }

        public bool isExist(Guid postId, string userId)
        {
            return _context.Posts.Any(e => e.Id == postId && e.UserId == userId);
        }
    }
}
