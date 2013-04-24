using System.Collections.Generic;
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

        [Test]
        public void ShouldDeleteBills()
        {
            const int billId = 123;
            var billContext = MockRepository.GenerateMock<IBillContext>();
            var bills = MockRepository.GenerateMock<IDbSet<BillModel>>();
            billContext.Stub(b => b.Bills).Return(bills);
            var billModel = new BillModel();
            bills.Stub(b => b.Find(billId)).Return(billModel);
            var billService = new BillService(billContext);

            billService.DeleteBill(billId);

            bills.AssertWasCalled(b => b.Remove(billModel));
            billContext.AssertWasCalled(b => b.SaveChanges());
        }
        
        [Test]
        public void ShouldModifyBills()
        {
            var billContext = MockRepository.GenerateMock<IBillContext>();
            var billModel = new BillModel();
            var billService = new BillService(billContext);

            billService.ModifyBill(billModel);

            billContext.AssertWasCalled(b => b.SetModified(billModel));
            billContext.AssertWasCalled(b => b.SaveChanges());
        }

        [Test]
        public void ShouldGetABill()
        {
            const int billId = 10;
            var billContext = MockRepository.GenerateMock<IBillContext>();
            var bills = MockRepository.GenerateMock<IDbSet<BillModel>>();
            billContext.Stub(b => b.Bills).Return(bills);
            var billModel = new BillModel();
            bills.Stub(b => b.Find(billId)).Return(billModel);
            var billService = new BillService(billContext);

            BillModel model = billService.GetBill(billId);

            Assert.That(model, Is.EqualTo(billModel));
        }

        [Test]
        public void ShouldDisposeTheContext()
        {
            var billContext = MockRepository.GenerateMock<IBillContext>();
            var billService = new BillService(billContext);

            billService.Dispose();

            billContext.AssertWasCalled(b => b.Dispose());
        }

        //TODO: Find a way to test this!
        public void ShouldGetBillsForAUser()
        {
            var billContext = MockRepository.GenerateMock<IBillContext>();
            var bills = MockRepository.GenerateMock<IDbSet<BillModel>>();

            billContext.Stub(b => b.Bills).Return(bills);
            var billService = new BillService(billContext);

            IEnumerable<BillModel> billsForUser = billService.GetBillsForUser(123);
        }
    }
}
