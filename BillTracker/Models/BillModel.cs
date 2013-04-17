using System;
using System.Data.Entity;

namespace BillTracker.Models
{
    public class BillModel
    {
        public int Id { get; set; }

        public string Vendor { get; set; }

        public DateTime StartFrom { get; set; }

        public Frequency Frequency { get; set; }

        public DateTime End { get; set; }

        public decimal DueAmount { get; set; }

        public ExpenseType ExpenseType { get; set; }

        public int UserId { get; set; }

    }

    public class BillContext : DbContext
    {
        public DbSet<BillModel> Bills { get; set; }
    }

    public class ExpenseType
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }


    public enum Frequency
    {
        Monthly,
        Annual,
        BiAnnual,
        Quarterly
    }
}