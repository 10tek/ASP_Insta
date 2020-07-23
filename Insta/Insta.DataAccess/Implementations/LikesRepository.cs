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
    public class LikesRepository : IRepository<Like>
    {
        private readonly DataContext _context;

        public LikesRepository(DataContext context)
        {
            _context = context;
        }

        public Task<Like> GetAsync(Guid id)
        {
            return _context.Likes.FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task<List<Like>> GetAllAsync()
        {
            return _context.Likes.ToListAsync();
        }

        public ValueTask<EntityEntry<Like>> CreateAsync(Like entity)
        {
            var createdLike = _context.Likes.AddAsync(entity);
            return createdLike;
        }

        public ValueTask<Like> EditAsync(Like entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Like>> GetAllByPostIdAsync(Guid id)
        {
            return _context.Likes.Where(x => x.PostId == id).ToListAsync();
        }

        public void Remove(Like entity)
        {
            var deletedLike = _context.Likes.FirstOrDefault(l => l.UserId == entity.UserId && l.PostId == entity.PostId);
            _context.Likes.Remove(deletedLike);
        }

        public bool Exist(Guid id)
        {
            return _context.Likes.Any(e => e.Id == id);
        }

        public bool isExist(Guid postId, string userId)
        {
            return _context.Likes.Any(e => e.PostId == postId && e.UserId == userId);
        }
    }
}
