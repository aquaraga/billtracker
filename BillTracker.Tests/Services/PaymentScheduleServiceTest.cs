using System;
using System.Collections.Generic;
using BillTracker.Models;
using BillTracker.Services;
using BillTracker.Tests.Helpers;
using NUnit.Framework;
using Rhino.Mocks;

namespace BillTracker.Tests.Services
{
    [TestFixture]
    public class PaymentScheduleServiceTest
    {
        [Test]
        public void ShouldFilterTheBillsForTheGivenUser()
        {
            var billContext = MockRepository.GenerateMock<IBillContext>();
            var now = DateTime.Now;
            var billModelForUser102 = new BillModel {UserId = 102, Id = 2, StartFrom = now.AddYears(-13), Repeat = new Repetition {RecurrenceNumber = 1, RecurrenceUnit = "Year"}};
            var bills = new InMemoryDbSet<BillModel>
                            {
                                new BillModel {UserId = 101, Id = 1, StartFrom = now.AddYears(-10) ,Repeat = new Repetition {RecurrenceNumber = 1, RecurrenceUnit = "Year"}},
                                billModelForUser102,
                                new BillModel {UserId = 103, Id = 3, StartFrom = now.AddYears(-12) ,Repeat = new Repetition {RecurrenceNumber = 1, RecurrenceUnit = "Year"}},
                            };
            billContext.Stub(c => c.Bills).Return(bills);

            var scheduleFilters = MockRepository.GenerateMock<IScheduleFilters>();
            var paymentScheduleService = new PaymentScheduleService(billContext, scheduleFilters);

            var scheduleRequest = new ScheduleRequest
            {
                StartDate = DateTime.Now.AddDays(-10),
                EndDate = DateTime.Now.AddDays(10),
                UserId = 102
            };

            var filteredBills = new List<BillModel> { billModelForUser102};
            scheduleFilters.Stub(f => f.Filter(
                Arg<IEnumerable<BillModel>>.Matches(b => b.MatchesAll(m => m.UserId == 102)), Arg<ScheduleRequest>.Is.Equal(scheduleRequest))).Return(filteredBills);

            var summaryOfDues = paymentScheduleService.GetSummaryOfDues(scheduleRequest);

            Assert.That(summaryOfDues.Bills, Is.EqualTo(filteredBills));
        }

        [Test]
        public void ShouldFilterRelevantBills()
        {
            var billContext = MockRepository.GenerateMock<IBillContext>();

            var scheduleFilters = MockRepository.GenerateMock<IScheduleFilters>();


            var billModel1 = new BillModel {UserId = 101, Id = 1};
            var billModel2 = new BillModel {UserId = 101, Id = 2};
            var billModel3 = new BillModel {UserId = 101, Id = 3};
            var bills = new InMemoryDbSet<BillModel>
                            {
                                billModel1,
                                billModel2,
                                billModel3,
                            };
            var scheduleRequest = new ScheduleRequest
            {
                StartDate = DateTime.Now.AddDays(-10),
                EndDate = DateTime.Now.AddDays(10),
                UserId = 101
            };
            billContext.Stub(c => c.Bills).Return(bills);
            var filteredBills = new List<BillModel> {billModel1, billModel3};
            scheduleFilters.Stub(f => f.Filter(Arg<IEnumerable<BillModel>>.Matches(b => bills.ContainsAll(b)), Arg<ScheduleRequest>.Is.Equal(scheduleRequest)))
                .Return(filteredBills);

            var paymentScheduleService = new PaymentScheduleService(billContext, scheduleFilters);

            
            var summaryOfDues = paymentScheduleService.GetSummaryOfDues(scheduleRequest);

            Assert.That(summaryOfDues.Bills, Is.EqualTo(filteredBills));
        }

//       
    }
}