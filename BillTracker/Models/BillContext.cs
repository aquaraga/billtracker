using System.Data.Entity;

namespace BillTracker.Models
{
    public class BillContext : DbContext, IBillContext
    {
        public IDbSet<BillModel> Bills { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<BillContext>());
            base.OnModelCreating(modelBuilder);
        }
    }
}