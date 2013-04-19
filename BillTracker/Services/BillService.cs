using BillTracker.Models;

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
    }
}