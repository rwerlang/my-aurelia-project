using Hahn.ApplicatonProcess.May2020.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.May2020.Domain.Models
{
    /// <summary>
    /// Model used the present applicant data
    /// </summary>
    /// <remarks>
    /// In a real world scenario, this approach separates the underlining table definition,
    /// in this case the Applicant table in the database, from the resulting view.
    /// This way, it is possible to evolve both separately without impact.
    /// </remarks>
    public class ApplicantViewModel
    {
        /// <summary>
        /// Unique Id of the applicant
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Applicant's name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Applicant's family name
        /// </summary>
        public string FamilyName { get; set; }
        /// <summary>
        /// Full address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// The country where the applicant is from
        /// </summary>
        public string CountryOfOrigin { get; set; }
        /// <summary>
        /// The applicant's email address. Must be a valid email.
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// Applicant's age
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// True if the applicant is hired. False otherwise.
        /// </summary>
        public bool Hired { get; set; }

        /// <summary>
        /// List of actions related to the object
        /// </summary>
        public List<RestLink> Links { get; set; }
    }
}
