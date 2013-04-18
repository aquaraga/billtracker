using BillTracker.Models;

namespace BillTracker.ViewModels.Mapper
{
    public interface IBillModelMapper
    {
        BillModel Map(BillViewModel billViewModel);
    }
}