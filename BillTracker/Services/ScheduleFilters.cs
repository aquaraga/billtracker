using System.Collections.Generic;
using System.Linq;
using BillTracker.Models;

namespace BillTracker.Services
{
    public class ScheduleFilters : IScheduleFilters
    {
        private readonly IEnumerable<IScheduleFilter> scheduleFilters;

        public ScheduleFilters(IEnumerable<IScheduleFilter> scheduleFilters)
        {
            this.scheduleFilters = scheduleFilters;
        }

        public IEnumerable<BillModel> Filter(IEnumerable<BillModel> billModels, ScheduleRequest request)
        {
            return scheduleFilters.Aggregate(billModels, (current, filter) => filter.Filter(current, request));
        }
    }
}