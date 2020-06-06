using Hahn.ApplicatonProcess.May2020.Domain.Entities;
using Hahn.ApplicatonProcess.May2020.Domain.Interfaces;
using Hahn.ApplicatonProcess.May2020.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.May2020.Data
{
    /// <summary>
    /// Repository for the Applicant entity
    /// </summary>
    public class ApplicantRepository : BaseEfRepository<DatabaseContext, Applicant>, IApplicantRepository
    {
        /// <summary>
        /// Base url for the API endpoint
        /// </summary>
        public string BaseEndpointUrl { get; set; }

        /// <summary>
        /// Creates a new instance of the object
        /// </summary>
        /// <param name="context">Database context</param>
        public ApplicantRepository(DatabaseContext context) : base(context)
        {
        }

        /// <summary>
        /// Returns a list of applicants ordered by first name
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>IReadOnlyList</returns>
        public async Task<IReadOnlyList<ApplicantViewModel>> ListByNameAsync(CancellationToken cancellationToken)
        {
            return await _context.Applicants
                .AsNoTracking()
                .OrderBy(a => a.Name)
                .Select(a => a.GetViewModel(BaseEndpointUrl))
                .ToListAsync();
        }

    }
}
