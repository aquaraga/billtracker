using System.Collections.Generic;
using BillTracker.Models;

namespace BillTracker.Services
{
    public interface IScheduleFilter
    {
        IEnumerable<BillModel> Filter(IEnumerable<BillModel> originalModels, ScheduleRequest scheduleRequest);
    }
}