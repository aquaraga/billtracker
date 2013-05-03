using System;
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
    public class PaymentScheduleControllerTest
    {
        [Test]
        public void ShouldInvokeScheduleServiceToGetEvents()
        {
            var paymentScheduleService = MockRepository.GenerateMock<IPaymentScheduleService>();
            var eventSummaryMapper = MockRepository.GenerateMock<IEventSummaryMapper>();
            var webSecurityWrapper = MockRepository.GenerateMock<IWebSecurityWrapper>();
            var paymentScheduleController = new PaymentScheduleController(paymentScheduleService, eventSummaryMapper, webSecurityWrapper);

            var startDate = new DateTime(2013, 4, 1);
            var endDate = new DateTime(2013, 4, 30);

            var scheduleSummary = new ScheduleSummary();
            var eventSummaryJsons = 
                new[] {new EventSummaryJson { title = "blah", start = "2013-4-12" }};

            webSecurityWrapper.Stub(w => w.GetUserId()).Return(1234);
            paymentScheduleService.Stub(
                pss => pss.GetSummaryOfDues(Arg<ScheduleRequest>.Matches(
                    sr => sr.StartDate.Equals(startDate) && sr.EndDate.Equals(endDate)
                    && sr.UserId == 1234)))
                .Return(scheduleSummary);
            eventSummaryMapper.Stub(es => es.Map(scheduleSummary)).Return(eventSummaryJsons);


            var eventSummary = paymentScheduleController.EventSummary(startDate.Ticks.ToString(), endDate.Ticks.ToString());

            Assert.That(eventSummary, Is.AssignableTo<JsonResult>());
            Assert.That(((JsonResult)eventSummary).Data, Is.EqualTo(eventSummaryJsons));

        }
    }
}
