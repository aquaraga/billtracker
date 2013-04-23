using System.Collections.Generic;
using BillTracker.Models;
using System.Linq;

namespace BillTracker.Services
{
    public class BillService : IBillService
    {
        private readonly IBillContext billContext;

        public BillService(IBillContext billContext)
        {
            this.billContext = billContext;
        }

        public void SaveBill(BillModel billModel)
        {
            billContext.Bills.Add(billModel);
            billContext.SaveChanges();
        }

        public IEnumerable<BillModel> GetBillsForUser(int userId)
        {
            return billContext.Bills.Where(b => b.UserId == userId);
        }
    }
}