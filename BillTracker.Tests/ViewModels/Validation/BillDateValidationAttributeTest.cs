using System;
using System.ComponentModel.DataAnnotations;
using BillTracker.ViewModels;
using BillTracker.ViewModels.Validation;
using NUnit.Framework;

namespace BillTracker.Tests.ViewModels.Validation
{
    [TestFixture]
    public class BillDateValidationAttributeTest
    {
        [Test, ExpectedException(typeof(ValidationException), ExpectedMessage = "End date cannot be less than start date")]
        public void ShouldValidateIfEndDateIsLessThanStartDate()
        {
            DateTime startDate = DateTime.Now.AddDays(10);
            DateTime endDate = DateTime.Now;

            var billDateValidationAttribute = new BillDateValidationAttribute();
            var billViewModel = new BillViewModel {StartFrom = startDate};
            var validationContext = new ValidationContext(billViewModel);

            billDateValidationAttribute.Validate(endDate, validationContext);
        }

        [Test]
        public void ValidationShouldGoThroughWhenEndDateAndStartDateAreSame()
        {
            DateTime startDate = DateTime.Now;
            DateTime endDate = startDate;

            var billDateValidationAttribute = new BillDateValidationAttribute();
            var billViewModel = new BillViewModel { StartFrom = startDate };
            var validationContext = new ValidationContext(billViewModel);

            billDateValidationAttribute.Validate(endDate, validationContext);
        }

        [Test]
        public void ValidationShouldGoThroughWhenEndDateIsGreaterThanStartDate()
        {
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now.AddDays(10);

            var billDateValidationAttribute = new BillDateValidationAttribute();
            var billViewModel = new BillViewModel { StartFrom = startDate };
            var validationContext = new ValidationContext(billViewModel);

            billDateValidationAttribute.Validate(endDate, validationContext);
        }
    }
}
