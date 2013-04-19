using BillTracker.Models;
using BillTracker.ViewModels;
using BillTracker.ViewModels.Mapper;
using NUnit.Framework;

namespace BillTracker.Tests.ViewModels.Mapper
{
    [TestFixture]
    public class FrequencyMapperTest
    {
        [Test]
        public void ShouldMapAnnualFrequency()
        {
            var frequencyMapper = new FrequencyMapper();

            Repeat repeat = frequencyMapper.Map(Frequency.Annual);

            Assert.That(repeat, Is.Not.Null);
            Assert.That(repeat.RecurrenceNumber, Is.EqualTo(1));
            Assert.That(repeat.RecurrenceUnit, Is.EqualTo("Year"));
        }

        [Test]
        public void ShouldMapBiAnnualFrequency()
        {
            var frequencyMapper = new FrequencyMapper();

            Repeat repeat = frequencyMapper.Map(Frequency.BiAnnual);

            Assert.That(repeat, Is.Not.Null);
            Assert.That(repeat.RecurrenceNumber, Is.EqualTo(6));
            Assert.That(repeat.RecurrenceUnit, Is.EqualTo("Month"));
        }

        [Test]
        public void ShouldMapMonthlyFrequency()
        {
            var frequencyMapper = new FrequencyMapper();

            Repeat repeat = frequencyMapper.Map(Frequency.Monthly);

            Assert.That(repeat, Is.Not.Null);
            Assert.That(repeat.RecurrenceNumber, Is.EqualTo(1));
            Assert.That(repeat.RecurrenceUnit, Is.EqualTo("Month"));
        }

        [Test]
        public void ShouldMapQuarterlyFrequency()
        {
            var frequencyMapper = new FrequencyMapper();

            Repeat repeat = frequencyMapper.Map(Frequency.Quarterly);

            Assert.That(repeat, Is.Not.Null);
            Assert.That(repeat.RecurrenceNumber, Is.EqualTo(3));
            Assert.That(repeat.RecurrenceUnit, Is.EqualTo("Month"));
        }
    }
}
