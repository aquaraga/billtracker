using BillTracker.Models;
using System.Linq;

namespace BillTracker.Services
{
    public class PaymentScheduleService : IPaymentScheduleService
    {
        private readonly IBillContext billContext;

        public PaymentScheduleService(IBillContext billContext)
        {
            this.billContext = billContext;
        }

        public ScheduleSummary GetSummaryOfDues(ScheduleRequest schedule)
        {
            var billModels = billContext.Bills.Where(b => b.UserId == schedule.UserId).ToList();
            
            
            var yearlyBills = billModels
                .Where(m => "Year".Equals(m.Repeat.RecurrenceUnit) && m.StartFrom.Month == schedule.Month)
                .ToList();

            return new ScheduleSummary {Bills = yearlyBills};
        }
    }
}