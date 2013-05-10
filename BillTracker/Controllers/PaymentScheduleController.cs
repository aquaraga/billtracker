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

        private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        //
        // GET: /PaymentSchedule/EventSummary

        public ActionResult EventSummary(string start, string end)
        {
            var userId = webSecurityWrapper.GetUserId();
            var startDate = UnixTimeStampToDateTime(Convert.ToDouble(start));
            var endDate = UnixTimeStampToDateTime(Convert.ToDouble(end));
            var summaryOfDues = paymentScheduleService.GetSummaryOfDues(
                new ScheduleRequest
                    {
                        UserId = userId,
                        StartDate = startDate,
                        EndDate = endDate
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
