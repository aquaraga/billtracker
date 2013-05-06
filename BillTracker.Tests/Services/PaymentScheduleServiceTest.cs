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
            var bills = new InMemoryDbSet<BillModel>
                            {
                                new BillModel {Id = 1, UserId = 101},
                                new BillModel {Id = 2, UserId = 102},
                                new BillModel {Id = 3, UserId = 101}
                            };
            billContext.Stub(c => c.Bills).Return(bills);

            var paymentScheduleService = new PaymentScheduleService(billContext);

            var scheduleRequest = new ScheduleRequest
                                      {
                                          UserId = 101
                                      };
            var summaryOfDues = paymentScheduleService.GetSummaryOfDues(scheduleRequest);

            Assert.That(summaryOfDues.Bills, Is.Not.Null);
            Assert.That(summaryOfDues.Bills.Count, Is.EqualTo(2));
            Assert.That(summaryOfDues.Bills.Exists(m => m.Id == 1), Is.True);
            Assert.That(summaryOfDues.Bills.Exists(m => m.Id == 3), Is.True);
        }
    }
}