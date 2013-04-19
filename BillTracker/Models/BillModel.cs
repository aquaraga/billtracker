using System;

namespace BillTracker.Models
{
    public class BillModel
    {
        public int Id { get; set; }

        public string Vendor { get; set; }

        public DateTime StartFrom { get; set; }

        public Repeat Repeat { get; set; }

        public DateTime End { get; set; }

        public decimal DueAmount { get; set; }

        public ExpenseType ExpenseType { get; set; }

        public int UserId { get; set; }

    }

    public class Repeat
    {
        public int RecurrenceNumber { get; set; }

        public string RecurrenceUnit { get; set; }
        
    }

    public class ExpenseType
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }


    
}