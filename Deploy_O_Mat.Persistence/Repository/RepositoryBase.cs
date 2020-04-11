using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Data.Context;
using com.b_velop.Deploy_O_Mat.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Deploy_O_Mat.Data.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class//, IEntity
    {
        protected ILogger<RepositoryBase<T>> Logger;
        protected readonly IList<IObserver<T>> Observers;
        protected readonly DataContext Context;

        protected string Key { get; set; }

        protected RepositoryBase(
            DataContext context,
            ILogger<RepositoryBase<T>> logger)
        {
            Context = context;
            Observers = new List<IObserver<T>>();
            Logger = logger;
        }

        public async Task<T> DeleteAsync(Guid id, CancellationToken cancellationToken = default, bool saveChanges = true)
        {
            var entity = await Context.Set<T>().FindAsync(id);
            var tmp = Context.Set<T>().Remove(entity);
            return tmp.Entity;
        }

        public T Insert(T entity, CancellationToken cancellationToken = default, bool saveChanges = true)
        {
            var e = Context.Set<T>().Add(entity);
            Context.SaveChanges();
            return e.Entity;
        }

        public int InsertRange(IEnumerable<T> entity, CancellationToken cancellationToken = default, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> SelectAllAsync(CancellationToken cancellationToken = default)
        {
            return await Context.Set<T>().ToListAsync();
        }

        public Task<IEnumerable<T>> SelectByConditionAsync(Func<T, bool> expression, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public T SelectById(Guid id)
        {
            return Context.Set<T>().Find(id);
        }

        public Task<T> SelectByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            throw new NotImplementedException();
        }

        public void UnsubscribeAll()
        {
            throw new NotImplementedException();
        }

        public abstract Task<bool> UpdateAsync(T entity, T old, CancellationToken cancellationToken = default);

        public Task<int> UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
    }
}
