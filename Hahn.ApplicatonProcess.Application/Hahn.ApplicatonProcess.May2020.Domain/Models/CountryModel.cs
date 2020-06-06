using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.May2020.Domain.Models
{
    /// <summary>
    /// Represents a country
    /// </summary>
    public class CountryModel
    {
        /// <summary>
        /// Contains the name of the country
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The unique code of the country
        /// </summary>
        public string Alpha3Code { get; set; }
    }
}
