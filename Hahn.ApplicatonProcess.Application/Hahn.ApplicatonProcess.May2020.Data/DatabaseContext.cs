using Hahn.ApplicatonProcess.May2020.Data.Metadata;
using Hahn.ApplicatonProcess.May2020.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Hahn.ApplicatonProcess.May2020.Data
{
    /// <summary>
    /// Database context for the applicant app
    /// </summary>
    public class DatabaseContext : DbContext
    {
        /// <summary>
        /// Represents the Applicant entity in the database
        /// </summary>
        public DbSet<Applicant> Applicants { get; set; }

        /// <summary>
        /// Creates a new instance of the object
        /// </summary>
        /// <param name="options">Configuration options for the context</param>
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Applicant>(ApplicantEntityBuilder.Build);
        }
    }
}
