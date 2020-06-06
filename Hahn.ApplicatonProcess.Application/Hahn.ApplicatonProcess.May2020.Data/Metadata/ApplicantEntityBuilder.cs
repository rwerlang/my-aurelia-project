using Hahn.ApplicatonProcess.May2020.Domain.Entities;
using Hahn.ApplicatonProcess.May2020.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hahn.ApplicatonProcess.May2020.Data.Metadata
{
    /// <summary>
    /// Class to build metadata attributes for the Applicant entity
    /// </summary>
    internal static class ApplicantEntityBuilder
    {
        /// <summary>
        /// Builds the metadata for the entity
        /// </summary>
        /// <param name="entity">Database entity</param>
        public static void Build(EntityTypeBuilder<Applicant> entity)
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            RequiredString(entity, "Name", ApplicantValidator.NameMaxLength);
            RequiredString(entity, "FamilyName", ApplicantValidator.FamilyNameMaxLength);
            RequiredString(entity, "Address", ApplicantValidator.AddressMaxLength);
            RequiredString(entity, "CountryOfOrigin", ApplicantValidator.CountryMaxLength);
            RequiredString(entity, "EmailAddress", ApplicantValidator.EmailMaxLength);
            
            entity.Property(e => e.Age).IsRequired();
            entity.Property(e => e.Hired).IsRequired();
        }

        private static void RequiredString(
            EntityTypeBuilder<Applicant> entity, 
            string prop, 
            int length)
        {
            entity.Property(prop)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(length);
        }
    }
}
