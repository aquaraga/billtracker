using System.Web.Mvc;
using BillTracker.Controllers;
using BillTracker.Models;
using BillTracker.ViewModels;
using BillTracker.ViewModels.Mapper;
using NUnit.Framework;
using Rhino.Mocks;

namespace BillTracker.Tests.Controllers
{
    [TestFixture]
    public class BillControllerTest
    {
        [Test]
        public void ShouldMapViewModelToModelWhileSavingABill()
        {
            var billModelMapper = MockRepository.GenerateMock<IBillModelMapper>();
            var billService = MockRepository.GenerateMock<IBillService>();
            var billController = new BillController(billModelMapper, billService);

            var billViewModel = new BillViewModel
                                    {
                                        Vendor = "Airtel"
                                    };
            var billModel = new BillModel();
            billModelMapper.Stub(mapper => mapper.Map(billViewModel)).Return(billModel);

            ActionResult result = billController.Create(billViewModel);

            Assert.That(result, Is.AssignableTo<RedirectToRouteResult>());
            Assert.That(((RedirectToRouteResult) result).RouteValues["action"], Is.EqualTo("Index"));
            billService.AssertWasCalled(c => c.SaveBill(billModel));
        }


    }
}
