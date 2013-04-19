using System.Data.Entity;

namespace BillTracker.Models
{
    public interface IBillContext
    {
        IDbSet<BillModel> Bills { get; set; }
        int SaveChanges();
    }
}