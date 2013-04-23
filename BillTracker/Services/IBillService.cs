using System.Collections.Generic;
using BillTracker.Models;

namespace BillTracker.Services
{
    public interface IBillService
    {
        void SaveBill(BillModel billModel);
        IEnumerable<BillModel> GetBillsForUser(int userId);
    }
}