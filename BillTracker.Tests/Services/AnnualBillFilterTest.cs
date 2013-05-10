using System;
using System.Collections.Generic;
using BillTracker.Models;
using BillTracker.Services;
using NUnit.Framework;
using System.Linq;

namespace BillTracker.Tests.Services
{
    [TestFixture]
    public class AnnualBillFilterTest
    {
        [Test]
        public void ShouldFilterYearlyBills()
        {
            var annualBillFilter = new AnnualBillFilter();
            var now = DateTime.Now;

            var repetitionForAnnualBills = new Repetition {RecurrenceNumber = 1, RecurrenceUnit = "Year"};
            var muchAheadInFuture = now.AddYears(40);
            var bills = new List<BillModel>
                            {
                                new BillModel {Id = 1, StartFrom = now.AddYears(-10) ,Repeat = repetitionForAnnualBills, End = muchAheadInFuture},
                                new BillModel {Id = 2, StartFrom = now.AddYears(-10).AddMonths(-5) ,Repeat = repetitionForAnnualBills, End = muchAheadInFuture},
                                new BillModel {Id = 3, StartFrom = now.AddYears(-10).AddMonths(-8) ,Repeat = repetitionForAnnualBills, End = muchAheadInFuture},
                            };

            var billModels = annualBillFilter.Filter(bills, new ScheduleRequest {StartDate = now.AddDays(-1), EndDate = now.AddDays(1)});

            Assert.That(billModels, Is.Not.Null);
            var models = billModels as IList<BillModel> ?? billModels.ToList();
            Assert.That(models.Count(), Is.EqualTo(1));
            Assert.That(models.First().Id, Is.EqualTo(1));
        }
    }
}
