using System.Data.Entity;
using BillTracker.Models;
using BillTracker.Services;
using NUnit.Framework;
using Rhino.Mocks;

namespace BillTracker.Tests.Services
{
    [TestFixture]
    public class BillServiceTest
    {
        [Test]
        public void ShouldSaveBills()
        {
            var billContext = MockRepository.GenerateMock<IBillContext>();
            var bills = MockRepository.GenerateMock<IDbSet<BillModel>>();
            billContext.Stub(b => b.Bills).Return(bills);
            var billService = new BillService(billContext);

            var billModel = new BillModel();
            billService.SaveBill(billModel);

            bills.AssertWasCalled(b => b.Add(billModel));
            billContext.AssertWasCalled(b => b.SaveChanges());
        }
    }
}
