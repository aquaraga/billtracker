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

        public BillModel GetBill(int billId)
        {
            return billContext.Bills.Find(billId);
        }

        public void ModifyBill(BillModel billModel)
        {
            billContext.SetModified(billModel);
            billContext.SaveChanges();
        }

        public void DeleteBill(int billId )
        {
            BillModel billmodel = billContext.Bills.Find(billId);
            billContext.Bills.Remove(billmodel);
            billContext.SaveChanges();
        }

        public void Dispose()
        {
            billContext.Dispose();
        }
    }
}