using FluentValidation;

namespace Hahn.ApplicatonProcess.May2020.Domain.Models
{
    /// <summary>
    /// Validator for Applicant
    /// </summary>
    public class ApplicantValidator : AbstractValidator<ApplicantInputModel>
    {
        /// <summary>Max length for name</summary>
        public const int NameMaxLength = 100;
        /// <summary>Max length for family name</summary>
        public const int FamilyNameMaxLength = 100;
        /// <summary>Max length for address</summary>
        public const int AddressMaxLength = 200;
        /// <summary>Max length for country</summary>
        public const int CountryMaxLength = 100;
        /// <summary>Max length for email</summary>
        public const int EmailMaxLength = 100;

        /// <summary>
        /// Creates a new instance of the object.
        /// </summary>
        /// <param name="countryService">Service used to validate a country</param>
        public ApplicantValidator(CountryService countryService)
        {
            RuleFor(r => r.Name).NotNull().MinimumLength(5).MaximumLength(NameMaxLength);
            RuleFor(r => r.FamilyName).NotNull().MinimumLength(5).MaximumLength(FamilyNameMaxLength);
            RuleFor(r => r.Address).NotNull().MinimumLength(10).MaximumLength(AddressMaxLength);
            RuleFor(r => r.EmailAddress).NotNull().EmailAddress().MaximumLength(EmailMaxLength);
            RuleFor(r => r.Age).NotNull().InclusiveBetween(20, 60);
            RuleFor(r => r.Hired).NotNull();

            RuleFor(r => r.CountryOfOrigin)
                .NotNull()
                .MaximumLength(CountryMaxLength)
                .CustomAsync(async (list, context, token) =>
                {
                    var country = await countryService.SearchCountryByNameAsync(context.PropertyValue.ToString());
                    if (country == null || country.Count == 0)
                    {
                        context.AddFailure("Invalid country");
                    }
                });
        }
    }
}
