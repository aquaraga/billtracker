using System.Collections.Generic;
using System.Linq;
using BillTracker.Models;

namespace BillTracker.Services
{
    public class AnnualBillFilter : IScheduleFilter
    {
        public IEnumerable<BillModel> Filter(IEnumerable<BillModel> originalModels, ScheduleRequest schedule)
        {

            var daysBetween = Enumerable.Range(0, 1 + schedule.EndDate.Subtract(schedule.StartDate).Days)
                                        .Select(offset => schedule.StartDate.AddDays(offset));
            
            var yearlyBills = originalModels
                .Where(m => "Year".Equals(m.Repeat.RecurrenceUnit))
                .ToList();
            
            
            var relevantBills = yearlyBills
                .Where(y => y.End >= schedule.EndDate 
                            && daysBetween.Select(d => d.Month).Distinct().Contains(y.StartFrom.Month));
            return relevantBills;
        }
    }
}