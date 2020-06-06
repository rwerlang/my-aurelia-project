using Hahn.ApplicatonProcess.May2020.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.May2020.Data
{
    /// <summary>
    /// Base EntityFramework class for the Repository pattern
    /// </summary>
    public class BaseEfRepository<TContext, TEntity> : IRepository<TEntity>
        where TContext : DbContext
        where TEntity: class, IEntity, IAggregateRoot
    {
        /// <summary>
        /// Database context
        /// </summary>
        protected readonly TContext _context;

        /// <summary>
        /// Creates a new instance of the object
        /// </summary>
        /// <param name="context">Database context</param>
        protected BaseEfRepository(TContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #region Read operations

        /// <summary>
        /// Returns a single entity
        /// </summary>
        /// <param name="id">The id to find the entity</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="trackChanges">Optional indicading to track changes</param>
        /// <returns>An instance of T</returns>
        public async virtual Task<TEntity> GetSingleByIdAsync(
            object id,
            CancellationToken cancellationToken,
            bool trackChanges = false)
        {
            bool autoDetectChanges = _context.ChangeTracker.AutoDetectChangesEnabled;

            if (!trackChanges)
            {
                _context.ChangeTracker.AutoDetectChangesEnabled = false;
            }

            try
            {
                var result = await _context.Set<TEntity>().FindAsync(new[] { id }, cancellationToken);
                return result;
            }
            finally
            {
                if (!trackChanges)
                {
                    _context.ChangeTracker.AutoDetectChangesEnabled = autoDetectChanges;
                }
            }
        }

        /// <summary>
        /// Returns a list of records
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="trackChanges">Optional indicading to track changes</param>
        /// <returns>A list of IReadOnlyList for the entity</returns>
        public async virtual Task<IReadOnlyList<TEntity>> ListAsync(
            CancellationToken cancellationToken, 
            bool trackChanges = false)
        {
            if (trackChanges)
            {
                return await _context.Set<TEntity>().ToListAsync(cancellationToken);
            }
            return await _context.Set<TEntity>().AsNoTracking().ToListAsync(cancellationToken);
        }

        #endregion

        #region Write operations

        /// <summary>
        /// Adds a new entity to the repository.
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>An instance of T</returns>
        public virtual TEntity Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            return entity;
        }

        /// <summary>
        /// Marks the given entity to be deleted
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        /// <summary>
        /// Persists the changes to the database.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Number of lines affected</returns>
        public async virtual Task<int> SaveUnitOfWorkAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);

            /* ORIGINAL METHOD, using domain events and the mediator pattern

            int result = default(int);
            var dispatchedEvents = await _mediator.DispatchTransactionEventsAsync(_context, cancellationToken);

            if (!cancellationToken.IsCancellationRequested)
            {
                result = await _context.SaveChangesAsync(cancellationToken);

                if (!cancellationToken.IsCancellationRequested)
                {
                    await _mediator.DispatchEventsAsync(dispatchedEvents, NotificationContext.Isolated, cancellationToken);
                }
            }

            return result;
            */
        }

        #endregion
    }
}
