using System;

namespace BillTracker.ViewModels
{
    public class BillViewModel
    {
        public string Vendor { get; set; }

        public DateTime StartFrom { get; set; }

        public Frequency Frequency { get; set; }

        public DateTime End { get; set; }

        public decimal DueAmount { get; set; }

    }

    public enum Frequency
    {
        Monthly,
        Annual,
        BiAnnual,
        Quarterly
    }
}