using Blog.Core.Entities;
using Blog.Data.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.UnitOfWorks
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<T> GetRepository<T>() where T : class, IEntityBase, new(); //Bütün repoları tek tek tanımlamak yerine bu satır sayesin de var olan bütün repoları tek bir noktadan yönetebileceğim.
        Task<int> SaveAsync();
        int Save();
    }
}
