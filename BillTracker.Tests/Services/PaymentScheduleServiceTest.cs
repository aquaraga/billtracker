using System;
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
        public void ShouldReturnThePaymentScheduleForAGivenUser()
        {
            var billContext = MockRepository.GenerateMock<IBillContext>();
            var now = DateTime.Now;
            var bills = new InMemoryDbSet<BillModel>
                            {
                                new BillModel {UserId = 101, Id = 1, StartFrom = now.AddYears(-10) ,Repeat = new Repetition {RecurrenceNumber = 1, RecurrenceUnit = "Year"}},
                                new BillModel {UserId = 102, Id = 2, StartFrom = now.AddYears(-13) ,Repeat = new Repetition {RecurrenceNumber = 1, RecurrenceUnit = "Year"}},
                                new BillModel {UserId = 101, Id = 3, StartFrom = now.AddYears(-12) ,Repeat = new Repetition {RecurrenceNumber = 1, RecurrenceUnit = "Year"}},
                            };
            billContext.Stub(c => c.Bills).Return(bills);

            var paymentScheduleService = new PaymentScheduleService(billContext);

            var scheduleRequest = new ScheduleRequest
            {
                Year = now.Year,
                Month = now.Month,
                UserId = 101
            };
            var summaryOfDues = paymentScheduleService.GetSummaryOfDues(scheduleRequest);

            Assert.That(summaryOfDues.Bills, Is.Not.Null);
            Assert.That(summaryOfDues.Bills.Count, Is.EqualTo(2));
            Assert.That(summaryOfDues.Bills.Exists(m => m.Id == 1), Is.True);
            Assert.That(summaryOfDues.Bills.Exists(m => m.Id == 3), Is.True);
        }

        [Test]
        public void ShouldReturnThePaymentScheduleForAnAnnualBill()
        {
            var billContext = MockRepository.GenerateMock<IBillContext>();
            var now = DateTime.Now;
            var bills = new InMemoryDbSet<BillModel>
                            {
                                new BillModel {Id = 1, StartFrom = now.AddYears(-10) ,Repeat = new Repetition {RecurrenceNumber = 1, RecurrenceUnit = "Year"}},
                                new BillModel {Id = 2, StartFrom = now.AddYears(-10).AddMonths(-5) ,Repeat = new Repetition {RecurrenceNumber = 1, RecurrenceUnit = "Year"}},
                                new BillModel {Id = 3, StartFrom = now.AddYears(-10).AddMonths(-8) ,Repeat = new Repetition {RecurrenceNumber = 1, RecurrenceUnit = "Year"}},
                            };
            billContext.Stub(c => c.Bills).Return(bills);

            var paymentScheduleService = new PaymentScheduleService(billContext);

            var scheduleRequest = new ScheduleRequest
            {
                Year = now.Year,
                Month = now.Month
            };
            var summaryOfDues = paymentScheduleService.GetSummaryOfDues(scheduleRequest);

            Assert.That(summaryOfDues.Bills, Is.Not.Null);
            Assert.That(summaryOfDues.Bills.Count, Is.EqualTo(1));
            Assert.That(summaryOfDues.Bills.Exists(m => m.Id == 1), Is.True);
        }
    }
}