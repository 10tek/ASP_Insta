using Insta.DataAccess.Interfaces;
using Insta.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Insta.DataAccess.Implementations
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public IRepository<Post> PostRepository { get; set; }
        public IRepository<Comment> CommentRepository { get; set; }
        public IRepository<Like> LikeRepository { get; set; }

        public EFUnitOfWork(IRepository<Post> postRepository, IRepository<Comment> commentRepository, IRepository<Like> likeRepository, DataContext context)
        {
            PostRepository = postRepository;
            CommentRepository = commentRepository;
            LikeRepository = likeRepository;
            _context = context;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task BeginTransaction()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            if (_context.Database.CurrentTransaction != null)
            {
                await _context.Database.CurrentTransaction.CommitAsync();
            }
        }

        public async Task Rollback()
        {
            if (_context.Database.CurrentTransaction != null)
            {
                await _context.Database.CurrentTransaction.RollbackAsync();
            }
        }
    }
}
