using BillTracker.Models;

namespace BillTracker.ViewModels.Mapper
{
    public class FrequencyMapper : IFrequencyMapper
    {
        public Repetition Map(Frequency frequency)
        {
            if (frequency.Equals(Frequency.Annual))
                return new Repetition { RecurrenceNumber = 1, RecurrenceUnit = "Year" };
            if (frequency.Equals(Frequency.BiAnnual))
                return new Repetition { RecurrenceNumber = 6, RecurrenceUnit = "Month" };
            if (frequency.Equals(Frequency.Quarterly))
                return new Repetition { RecurrenceNumber = 3, RecurrenceUnit = "Month" };
            if (frequency.Equals(Frequency.Monthly))
                return new Repetition { RecurrenceNumber = 1, RecurrenceUnit = "Month" };
            return new Repetition { RecurrenceNumber = 0, RecurrenceUnit = "Day"};
        }

        public Frequency Map(Repetition repetition)
        {
            if (repetition.RecurrenceUnit.Equals("Year"))
            {
                return Frequency.Annual;
            }
            if (repetition.RecurrenceUnit.Equals("Month"))
            {
                return repetition.RecurrenceNumber == 6
                           ? Frequency.BiAnnual
                           : repetition.RecurrenceNumber == 3 ? Frequency.Quarterly : Frequency.Monthly;
            }
            return Frequency.OneTime;
        }
    }
}