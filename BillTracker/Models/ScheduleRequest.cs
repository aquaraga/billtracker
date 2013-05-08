using System;

namespace BillTracker.Models
{
    public class ScheduleRequest
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int UserId { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }
    }
}