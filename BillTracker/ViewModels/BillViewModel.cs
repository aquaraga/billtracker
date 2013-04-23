﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using BillTracker.ViewModels.Validation;

namespace BillTracker.ViewModels
{
    public class BillViewModel
    {
        [Required]
        public string Vendor { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartFrom { get; set; }


        public Frequency Frequency { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [BillDateValidation]
        public DateTime End { get; set; }

        public decimal DueAmount { get; set; }

        public SelectList FrequencyList
        {
            get
            {
                IEnumerable<Repeat> repetitions = GetFrequencies();
                return new SelectList(repetitions, "Frequency", "Value", null);
            }
        }

        public int Id { get; set; }

        private IEnumerable<Repeat> GetFrequencies()
        {
           return new List<Repeat>
                      {
                    new Repeat {Frequency = Frequency.Monthly, Value = "Monthly"},
                    new Repeat {Frequency = Frequency.Quarterly, Value = "Quarterly"},
                    new Repeat {Frequency = Frequency.BiAnnual, Value = "BiAnnual"},
                    new Repeat {Frequency = Frequency.Annual, Value = "Annual"},
                    new Repeat {Frequency = Frequency.OneTime, Value = "One time"},
                };
        }
    }

    public class Repeat
    {
        public Frequency Frequency { get; set; }
        public string Value { get; set; }
    }

    public enum Frequency
    {
        Monthly,
        Annual,
        BiAnnual,
        Quarterly,
        OneTime
    }
}