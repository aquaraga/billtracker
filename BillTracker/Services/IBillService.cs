using BillTracker.Models;

namespace BillTracker.Services
{
    public interface IBillService
    {
        void SaveBill(BillModel billModel);
    }
}