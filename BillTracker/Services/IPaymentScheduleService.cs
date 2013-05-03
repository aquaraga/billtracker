using BillTracker.Models;

namespace BillTracker.Services
{
    public interface IPaymentScheduleService
    {
        ScheduleSummary GetSummaryOfDues(ScheduleRequest anything);
    }
}