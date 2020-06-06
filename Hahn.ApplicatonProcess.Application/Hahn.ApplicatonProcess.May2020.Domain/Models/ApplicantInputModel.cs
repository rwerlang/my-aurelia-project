using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hahn.ApplicatonProcess.May2020.Domain.Models
{
    /// <summary>
    /// Model used in data entry operations
    /// </summary>
    /// <remarks>
    /// In a real world scenario, this approach separates the underlining table definition,
    /// in this case the Applicant table in the database, from the input.
    /// This way, it is possible to evolve both separately.
    /// </remarks>
    public class ApplicantInputModel
    {
        /// <summary>
        /// Applicant's name
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Applicant's family name
        /// </summary>
        [Required]
        public string FamilyName { get; set; }
        /// <summary>
        /// Full address
        /// </summary>
        [Required]
        public string Address { get; set; }
        /// <summary>
        /// The country where the applicant is from
        /// </summary>
        [Required]
        public string CountryOfOrigin { get; set; }
        /// <summary>
        /// The applicant's email address. Must be a valid email.
        /// </summary>
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        /// <summary>
        /// Applicant's age. Must be between 20 and 60.
        /// </summary>
        [Required]
        public int Age { get; set; }
        /// <summary>
        /// True if the applicant is hired. False otherwise.
        /// </summary>
        [Required]
        public bool Hired { get; set; } = false;
    }
}
