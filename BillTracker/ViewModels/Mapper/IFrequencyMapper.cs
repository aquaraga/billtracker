using BillTracker.Models;

namespace BillTracker.ViewModels.Mapper
{
    public interface IFrequencyMapper
    {
        Repeat Map(Frequency frequency);
    }
}