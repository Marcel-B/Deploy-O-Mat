using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace com.b_velop.Deploy_O_Mat.Domain.Interfaces
{
    public interface IRepositoryBase<T> : IObservable<T>
    {
        T Insert(T entity, CancellationToken cancellationToken = default, bool saveChanges = true);
        int InsertRange(IEnumerable<T> entity, CancellationToken cancellationToken = default, bool saveChanges = true);
        Task<int> UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default, bool saveChanges = true);
        Task<IEnumerable<T>> SelectAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> SelectByConditionAsync(Func<T, bool> expression, CancellationToken cancellationToken = default);
        Task<T> SelectByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(T entity, T old, CancellationToken cancellationToken = default);
        Task<T> DeleteAsync(Guid id, CancellationToken cancellationToken = default, bool saveChanges = true);
        T SelectById(Guid id);
        void UnsubscribeAll();
    }
}
