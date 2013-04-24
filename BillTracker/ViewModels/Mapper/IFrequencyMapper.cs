using BillTracker.Models;

namespace BillTracker.ViewModels.Mapper
{
    public interface IFrequencyMapper
    {
        Repetition Map(Frequency frequency);
        Frequency Map(Repetition repetition);
    }
}