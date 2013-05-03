using System;
using System.Data.Entity;
using BillTracker.Models;
using BillTracker.Services;
using NUnit.Framework;
using Rhino.Mocks;

namespace BillTracker.Tests.Services
{
    [TestFixture]
    public class PaymentScheduleServiceTest
    {
        [Test, Ignore("Revisit this test")]
        public void ShouldReturnThePaymentScheduleForAGivenUser()
        {
            var billContext = MockRepository.GenerateMock<IBillContext>();
            var bills = MockRepository.GenerateMock<IDbSet<BillModel>>();
            billContext.Stub(c => c.Bills).Return(bills);

            var paymentScheduleService = new PaymentScheduleService(billContext);

            var scheduleRequest = new ScheduleRequest
                                      {
                                          StartDate = DateTime.Now.AddDays(-10), EndDate = DateTime.Now, UserId = 1234
                                      };
            var summaryOfDues = paymentScheduleService.GetSummaryOfDues(scheduleRequest);


        }
    }
}