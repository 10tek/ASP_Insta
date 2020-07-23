using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Insta.DataAccess.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(Guid id);
        Task<List<T>> GetAllAsync();
        ValueTask<EntityEntry<T>> CreateAsync(T entity);
        ValueTask<T> EditAsync(T entity);
        Task<List<T>> GetAllByPostIdAsync(Guid id);
        void Remove(T entity);
        bool Exist(Guid id);
        bool isExist(Guid postId, string userId);
    }
}
