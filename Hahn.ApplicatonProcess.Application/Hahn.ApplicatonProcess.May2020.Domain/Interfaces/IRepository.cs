using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.May2020.Domain.Interfaces
{
    /// <summary>
    /// Interface to implement the repository pattern
    /// </summary>
    /// <typeparam name="T">An entity to use in the repository</typeparam>
    public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T>
        where T: class, IEntity, IAggregateRoot
    {
    }
}
