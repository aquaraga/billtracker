using System;
using System.ComponentModel.DataAnnotations;

namespace BillTracker.ViewModels.Validation
{
    public class BillDateValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object endDate, ValidationContext validationContext)
        {
            if (endDate is DateTime)
            {
                var billViewModel = (BillViewModel) validationContext.ObjectInstance;
                if (billViewModel.StartFrom > (DateTime)endDate)
                    return new ValidationResult("End date cannot be less than start date");
            }
            return ValidationResult.Success;
        }
    }
}