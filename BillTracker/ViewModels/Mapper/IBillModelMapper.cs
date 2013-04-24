using BillTracker.Models;

namespace BillTracker.ViewModels.Mapper
{
    public interface IBillModelMapper
    {
        BillModel Map(BillViewModel billViewModel, int userId);
        BillViewModel Map(BillModel billModel);
        void Extend(BillModel billModel, BillViewModel billViewModel);
    }
}