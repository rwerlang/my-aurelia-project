using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.May2020.Domain.Interfaces
{
    /// <summary>
    /// Interface that defines methods to write to an entity using the repository pattern
    /// </summary>
    /// <typeparam name="T">An entity to use in the repository</typeparam>
    public interface IWriteRepository<T> where T : class, IEntity, IAggregateRoot
    {
        /// <summary>
        /// Adds a new entity to the repository.
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>An instance of T</returns>
        T Add(T entity);
        /// <summary>
        /// Marks the given entity to be deleted
        /// </summary>
        /// <param name="entity">Entity</param>
        void Delete(T entity);
        /// <summary>
        /// Persists the changes to the database.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Number of lines affected</returns>
        Task<int> SaveUnitOfWorkAsync(CancellationToken cancellationToken);
    }
}
