using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.May2020.Domain.Interfaces
{
    /// <summary>
    /// Interface that defines read operations for the repository pattern
    /// </summary>
    /// <typeparam name="T">An entity to use in the repository</typeparam>
    /// <remarks>
    /// NOTE: I left out some other definitions for the sake of simplification.
    /// </remarks>
    public interface IReadRepository<T> where T: class, IEntity, IAggregateRoot
    {
        /// <summary>
        /// Returns a single entity
        /// </summary>
        /// <param name="id">The id to find the entity</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="trackChanges">Optional indicading to track changes</param>
        /// <returns>An instance of T</returns>
        Task<T> GetSingleByIdAsync(object id, CancellationToken cancellationToken, bool trackChanges = false);

        /// <summary>
        /// Returns a list of records
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="trackChanges">Optional indicading to track changes</param>
        /// <returns>A list of IReadOnlyList for the entity</returns>
        Task<IReadOnlyList<T>> ListAsync(CancellationToken cancellationToken, bool trackChanges = false);
    }
}
