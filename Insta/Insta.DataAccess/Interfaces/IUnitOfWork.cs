using Insta.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Insta.DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Post> PostRepository { get; set; }
        IRepository<Comment> CommentRepository { get; set; }
        IRepository<Like> LikeRepository { get; set; }


        Task Save();
        Task BeginTransaction();
        Task Commit();
        Task Rollback();
    }
}
