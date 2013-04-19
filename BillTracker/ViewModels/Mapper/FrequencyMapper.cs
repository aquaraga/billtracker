using BillTracker.Models;

namespace BillTracker.ViewModels.Mapper
{
    public class FrequencyMapper : IFrequencyMapper
    {
        public Repeat Map(Frequency frequency)
        {
            if (frequency.Equals(Frequency.Annual))
                return new Repeat {RecurrenceNumber = 1, RecurrenceUnit = "Year"};
            if (frequency.Equals(Frequency.BiAnnual))
                return new Repeat { RecurrenceNumber = 6, RecurrenceUnit = "Month" };
            if (frequency.Equals(Frequency.Quarterly))
                return new Repeat { RecurrenceNumber = 3, RecurrenceUnit = "Month" };
            if (frequency.Equals(Frequency.Monthly))
                return new Repeat { RecurrenceNumber = 1, RecurrenceUnit = "Month" };
            return null;
        }
    }
}