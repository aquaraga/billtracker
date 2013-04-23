using System.Collections.Generic;
using System.Data.Entity;
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
