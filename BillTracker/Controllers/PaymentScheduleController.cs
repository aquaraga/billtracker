using System;
using System.Web.Mvc;
using BillTracker.Models;
using BillTracker.Services;
using BillTracker.ViewModels.Mapper;

namespace BillTracker.Controllers
{
    public class PaymentScheduleController : Controller
    {
        private readonly IPaymentScheduleService paymentScheduleService;
        private readonly IEventSummaryMapper eventSummaryMapper;
        private readonly IWebSecurityWrapper webSecurityWrapper;

        public PaymentScheduleController(IPaymentScheduleService paymentScheduleService, IEventSummaryMapper eventSummaryMapper, IWebSecurityWrapper webSecurityWrapper)
        {
            this.paymentScheduleService = paymentScheduleService;
            this.eventSummaryMapper = eventSummaryMapper;
            this.webSecurityWrapper = webSecurityWrapper;
        }

        //
        // GET: /PaymentSchedule/EventSummary

        public ActionResult EventSummary(string start, string end)
        {
            var userId = webSecurityWrapper.GetUserId();
            var summaryOfDues = paymentScheduleService.GetSummaryOfDues(
                new ScheduleRequest
                    {
                        Year = new DateTime(Convert.ToInt64(start)).Year,
                        Month = new DateTime(Convert.ToInt64(start)).Month, 
                        UserId = userId
                    });

            var eventSummaryJsons = eventSummaryMapper.Map(summaryOfDues);
            return Json(eventSummaryJsons, JsonRequestBehavior.AllowGet);

//            var eventDetails = new
//            {
//                title = "sample event",
//                start = "2013-04-30",
//                id = "0"
//            };
//            return Json(new[] { eventDetails }, JsonRequestBehavior.AllowGet);

//            return Json(new object[] {  }, JsonRequestBehavior.AllowGet);
        }

    }
}
