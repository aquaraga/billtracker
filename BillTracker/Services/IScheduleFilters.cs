using System.Collections.Generic;
using BillTracker.Models;

namespace BillTracker.Services
{
    public interface IScheduleFilters
    {
        IEnumerable<BillModel> Filter(IEnumerable<BillModel> billModels, ScheduleRequest scheduleRequest);
    }
}