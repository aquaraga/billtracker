using System.Collections.Generic;
using System.Web.Mvc;
using BillTracker.Controllers;
using BillTracker.Models;
using BillTracker.Services;
using BillTracker.ViewModels;
using BillTracker.ViewModels.Mapper;
using NUnit.Framework;
using Rhino.Mocks;

namespace BillTracker.Tests.Controllers
{
    [TestFixture]
    public class BillControllerTest
    {
        private IBillModelMapper billModelMapper;
        private IBillService billService;
        private BillController billController;
        private IWebSecurityWrapper webSecurityWrapper;

        [SetUp]
        public void Setup()
        {
            billModelMapper = MockRepository.GenerateMock<IBillModelMapper>();
            billService = MockRepository.GenerateMock<IBillService>();
            webSecurityWrapper = MockRepository.GenerateMock<IWebSecurityWrapper>();
            billController = new BillController(billModelMapper, billService, webSecurityWrapper);
        }


        [Test]
        public void ShouldMapViewModelToModelWhileSavingABill()
        {
            webSecurityWrapper.Stub(w => w.GetUserId()).Return(123);
            var billViewModel = new BillViewModel
                                    {
                                        Vendor = "Airtel"
                                    };
            var billModel = new BillModel();
            billModelMapper.Stub(mapper => mapper.Map(billViewModel, 123)).Return(billModel);

            ActionResult result = billController.Create(billViewModel);

            Assert.That(result, Is.AssignableTo<RedirectToRouteResult>());
            Assert.That(((RedirectToRouteResult) result).RouteValues["action"], Is.EqualTo("Index"));
            billService.AssertWasCalled(c => c.SaveBill(billModel));
        }

        [Test]
        public void ShouldBeAbleToSaveABillAfterEditing()
        {
            var billViewModel = new BillViewModel
            {
                Vendor = "Airtel",
                Id = 123
            };
            var billModel = new BillModel();

            billService.Stub(s => s.GetBill(123)).Return(billModel);

            ActionResult result = billController.Edit(billViewModel);

            Assert.That(result, Is.AssignableTo<RedirectToRouteResult>());
            Assert.That(((RedirectToRouteResult)result).RouteValues["action"], Is.EqualTo("Index"));
            billService.AssertWasCalled(c => c.ModifyBill(billModel));
            billModelMapper.AssertWasCalled(mapper => mapper.Extend(billModel, billViewModel));
        }


        [Test]
        public void ShouldGetDetailsOfABill()
        {
            var billViewModel = new BillViewModel
            {
                Vendor = "Airtel"
            };
            var billModel = new BillModel();

            const int billId = 10;
            billService.Stub(s => s.GetBill(billId)).Return(billModel);
            billModelMapper.Stub(mapper => mapper.Map(billModel)).Return(billViewModel);

            ActionResult actionResult = billController.Details(billId);

            Assert.That(actionResult, Is.AssignableTo<ViewResult>());
            Assert.That(((ViewResult)actionResult).Model, Is.EqualTo(billViewModel));
        }

        [Test]
        public void ShouldGetDetailsOfABillInEditMode()
        {
            var billViewModel = new BillViewModel
            {
                Vendor = "Airtel"
            };
            var billModel = new BillModel();

            const int billId = 10;
            billService.Stub(s => s.GetBill(billId)).Return(billModel);
            billModelMapper.Stub(mapper => mapper.Map(billModel)).Return(billViewModel);

            ActionResult actionResult = billController.Edit(billId);

            Assert.That(actionResult, Is.AssignableTo<ViewResult>());
            Assert.That(((ViewResult)actionResult).Model, Is.EqualTo(billViewModel));
        }

        [Test] 
        public void ShouldGetDetailsOfABillPriorToDeletion()
        {
            var billViewModel = new BillViewModel
            {
                Vendor = "Airtel"
            };
            var billModel = new BillModel();

            const int billId = 10;
            billService.Stub(s => s.GetBill(billId)).Return(billModel);
            billModelMapper.Stub(mapper => mapper.Map(billModel)).Return(billViewModel);

            ActionResult actionResult = billController.Delete(billId);

            Assert.That(actionResult, Is.AssignableTo<ViewResult>());
            Assert.That(((ViewResult)actionResult).Model, Is.EqualTo(billViewModel));
        }


        [Test]
        public void ShouldDeleteABill()
        {
            ActionResult result = billController.DeleteConfirmed(123);

            Assert.That(result, Is.AssignableTo<RedirectToRouteResult>());
            Assert.That(((RedirectToRouteResult)result).RouteValues["action"], Is.EqualTo("Index"));
            billService.AssertWasCalled(c => c.DeleteBill(123));
        }

        [Test]
        public void ShouldGetBillsForTheLoggedInUser()
        {
            webSecurityWrapper.Stub(w => w.GetUserId()).Return(123);
            var billModel1 = MockRepository.GenerateMock<BillModel>();
            var billModel2 = MockRepository.GenerateMock<BillModel>();
            var billModels = new List<BillModel>
                                 {
                                     billModel1,
                                     billModel2
                                 };
            billService.Stub(s => s.GetBillsForUser(123)).Return(billModels);

            

            var billViewModel1 = MockRepository.GenerateMock<BillViewModel>();
            var billViewModel2 = MockRepository.GenerateMock<BillViewModel>();

            billModelMapper.Stub(m => m.Map(billModel1)).Return(billViewModel1);
            billModelMapper.Stub(m => m.Map(billModel2)).Return(billViewModel2);

            ActionResult actionResult = billController.Index();

            Assert.That(actionResult, Is.AssignableTo<ViewResult>());
            Assert.That(((ViewResult) actionResult).Model, Is.EqualTo(new List<BillViewModel> {billViewModel1, billViewModel2}));
        }


    }
}
