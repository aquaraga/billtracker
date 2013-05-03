using BillTracker.Models;
using System.Linq;

namespace BillTracker.ViewModels.Mapper
{
    public class EventSummaryMapper : IEventSummaryMapper
    {
        public EventSummaryJson[] Map(ScheduleSummary scheduleSummary)
        {
            return scheduleSummary.Bills.Select(b => new EventSummaryJson { title = b.Vendor, start = "2013-05-10" }).ToArray();
        }
    }
}