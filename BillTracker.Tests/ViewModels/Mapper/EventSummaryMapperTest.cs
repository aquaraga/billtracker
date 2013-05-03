using System.Collections.Generic;
using BillTracker.Models;
using BillTracker.ViewModels.Mapper;
using NUnit.Framework;

namespace BillTracker.Tests.ViewModels.Mapper
{
    [TestFixture]
    public class EventSummaryMapperTest
    {
        [Test]
        public void ShouldMapAPaymentScheduleSummary()
        {
            var eventSummaryMapper = new EventSummaryMapper();

            var eventSummaryJsons = eventSummaryMapper.Map(new ScheduleSummary
                                                               {
                                                                   Bills = new List<BillModel>
                                                                               {
                                                                                   new BillModel {Vendor = "Airtel"}
                                                                               }
                                                               });
            Assert.That(eventSummaryJsons.Length, Is.EqualTo(1));
            Assert.That(eventSummaryJsons[0].title, Is.EqualTo("Airtel"));
        }
    }
}