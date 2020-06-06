using Hahn.ApplicatonProcess.May2020.Domain.Entities;
using Hahn.ApplicatonProcess.May2020.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.May2020.Domain.Interfaces
{
    /// <summary>
    /// Repository definition for the Applicant entity
    /// </summary>
    public interface IApplicantRepository : IRepository<Applicant>
    {
        /// <summary>
        /// Base url for the API endpoint
        /// </summary>
        string BaseEndpointUrl { get; set; }

        /// <summary>
        /// Returns a list of applicants
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>IReadOnlyList</returns>
        Task<IReadOnlyList<ApplicantViewModel>> ListByNameAsync(CancellationToken cancellationToken);
    }
}
