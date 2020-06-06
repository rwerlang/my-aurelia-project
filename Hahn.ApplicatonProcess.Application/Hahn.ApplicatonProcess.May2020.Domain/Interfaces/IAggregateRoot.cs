using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.May2020.Domain.Interfaces
{
    /// <summary>
    /// This interface is used to mark an entity as an aggregation root.
    /// </summary>
    /// <remarks>
    /// For instance:
    /// 
    /// Order
    /// OrderItems
    /// 
    /// The second one can not exist without the first class.
    /// Therefore, Order is the aggregate root and only aggregate roots can be used inside a repository.
    /// </remarks>
    public interface IAggregateRoot
    {
    }
}
