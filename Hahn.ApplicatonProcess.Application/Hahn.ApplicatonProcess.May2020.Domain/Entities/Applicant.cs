using Hahn.ApplicatonProcess.May2020.Domain.Interfaces;
using Hahn.ApplicatonProcess.May2020.Domain.Models;
using System;
using System.Collections.Generic;

namespace Hahn.ApplicatonProcess.May2020.Domain.Entities
{
    /// <summary>
    /// This class describes the properties of an applicant of a hiring process
    /// </summary>
    public class Applicant: IEntity, IAggregateRoot
    {
        /// <summary>
        /// Unique Id of the applicant
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Applicant's name
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Applicant's family name
        /// </summary>
        public string FamilyName { get; private set; }
        /// <summary>
        /// Full address
        /// </summary>
        public string Address { get; private set; }
        /// <summary>
        /// The country where the applicant is from
        /// </summary>
        public string CountryOfOrigin { get; private set; }
        /// <summary>
        /// The applicant's email address. Must be a valid email.
        /// </summary>
        public string EmailAddress { get; private set; }
        /// <summary>
        /// Applicant's age
        /// </summary>
        public int Age { get; private set; }
        /// <summary>
        /// True if the applicant is hired. False otherwise.
        /// </summary>
        public bool Hired { get; private set; }

        /// <summary>
        /// Creates a new instance of the object
        /// </summary>
        public Applicant()
        {
        }

        /// <summary>
        /// Creates a new instance of the object
        /// </summary>
        /// <param name="model">Object containing input data</param>
        public Applicant(ApplicantInputModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            UpdateFromModel(model);
        }

        /// <summary>
        /// Returns the ApplicantViewModel object for this instance.
        /// </summary>
        /// <param name="baseEndpointUrl">The base endpoint url.</param>
        /// <returns>ApplicantViewModel</returns>
        public ApplicantViewModel GetViewModel(string baseEndpointUrl)
        {
            return new ApplicantViewModel
            {
                Id = Id,
                Name = Name,
                FamilyName = FamilyName,
                EmailAddress = EmailAddress,
                Address = Address,
                Age = Age,
                CountryOfOrigin = CountryOfOrigin,
                Hired = Hired,
                Links = new List<RestLink>
                {
                    new RestLink { Action = "GET", Href = $"{baseEndpointUrl}/applicants/{Id}" },
                    new RestLink { Action = "PUT", Href = $"{baseEndpointUrl}/applicants/{Id}" },
                    new RestLink { Action = "DELETE", Href = $"{baseEndpointUrl}/applicants/{Id}" }
                }
            };
        }

        /// <summary>
        /// Updates the current instance using the given model.
        /// </summary>
        /// <param name="model">Object with the applicant input data</param>
        public void UpdateFromModel(ApplicantInputModel model)
        {
            Name = model.Name;
            FamilyName = model.FamilyName;
            Address = model.Address;
            Age = model.Age;
            CountryOfOrigin = model.CountryOfOrigin;
            EmailAddress = model.EmailAddress;
            Hired = model.Hired;
        }
    }
}
