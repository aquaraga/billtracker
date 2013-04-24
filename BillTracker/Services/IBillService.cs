using System.Collections.Generic;
using BillTracker.Models;

namespace BillTracker.Services
{
    public interface IBillService
    {
        void SaveBill(BillModel billModel);
        IEnumerable<BillModel> GetBillsForUser(int userId);
        BillModel GetBill(int billId);
        void ModifyBill(BillModel billModel);
        void DeleteBill(int billId);
        void Dispose();
    }
}