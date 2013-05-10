using BillTracker.Models;
using System.Linq;

namespace BillTracker.Services
{
    public class PaymentScheduleService : IPaymentScheduleService
    {
        private readonly IBillContext billContext;
        private readonly IScheduleFilters scheduleFilters;

        public PaymentScheduleService(IBillContext billContext, IScheduleFilters scheduleFilters)
        {
            this.billContext = billContext;
            this.scheduleFilters = scheduleFilters;
        }

        public ScheduleSummary GetSummaryOfDues(ScheduleRequest schedule)
        {
            var billModels = billContext.Bills.Where(b => b.UserId == schedule.UserId);

            var filteredBills = scheduleFilters.Filter(billModels, schedule).ToList();
            return new ScheduleSummary {Bills = filteredBills};
        }
    }
}