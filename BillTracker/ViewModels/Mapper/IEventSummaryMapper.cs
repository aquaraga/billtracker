using BillTracker.Models;

namespace BillTracker.ViewModels.Mapper
{
    public interface IEventSummaryMapper
    {
        EventSummaryJson[] Map(ScheduleSummary scheduleSummary);
    }
}