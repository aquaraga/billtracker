using System.Collections.Generic;
using BillTracker.Models;
using BillTracker.Services;
using NUnit.Framework;
using Rhino.Mocks;

namespace BillTracker.Tests.Services
{
    [TestFixture]
    public class ScheduleFiltersTest
    {
        [Test]
        public void ShouldConsolidateResultsOfEachFilter()
        {
            var scheduleRequest = MockRepository.GenerateMock<ScheduleRequest>();
            var filter1 = MockRepository.GenerateMock<IScheduleFilter>();
            var filter2 = MockRepository.GenerateMock<IScheduleFilter>();
            var scheduleFilters = new ScheduleFilters(new List<IScheduleFilter>
                                                          {
                                                              filter1,
                                                              filter2,
                                                          });

            var originalModels = MockRepository.GenerateMock<IList<BillModel>>();
            var firstFilterResults = MockRepository.GenerateMock<IList<BillModel>>();
            var secondFilterResults = MockRepository.GenerateMock<IList<BillModel>>();
            filter1.Stub(f => f.Filter(originalModels, scheduleRequest)).Return(firstFilterResults);
            filter2.Stub(f => f.Filter(firstFilterResults, scheduleRequest)).Return(secondFilterResults);


            var billModels = scheduleFilters.Filter(originalModels, scheduleRequest);

            Assert.That(billModels, Is.EqualTo(secondFilterResults));
        }
    }
}
