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

            Repetition repeat = frequencyMapper.Map(Frequency.Annual);

            Assert.That(repeat, Is.Not.Null);
            Assert.That(repeat.RecurrenceNumber, Is.EqualTo(1));
            Assert.That(repeat.RecurrenceUnit, Is.EqualTo("Year"));
        }

        [Test]
        public void ShouldMapAnnualRepetition()
        {
            var frequencyMapper = new FrequencyMapper();

            Frequency frequency = frequencyMapper.Map(new Repetition {RecurrenceNumber = 1, RecurrenceUnit = "Year"});

            Assert.That(frequency, Is.EqualTo(Frequency.Annual));
        }

        [Test]
        public void ShouldMapBiAnnualFrequency()
        {
            var frequencyMapper = new FrequencyMapper();

            Repetition repeat = frequencyMapper.Map(Frequency.BiAnnual);

            Assert.That(repeat, Is.Not.Null);
            Assert.That(repeat.RecurrenceNumber, Is.EqualTo(6));
            Assert.That(repeat.RecurrenceUnit, Is.EqualTo("Month"));
        }

        [Test]
        public void ShouldMapBiAnnualRepetition()
        {
            var frequencyMapper = new FrequencyMapper();

            Frequency frequency = frequencyMapper.Map(new Repetition { RecurrenceNumber = 6, RecurrenceUnit = "Month" });

            Assert.That(frequency, Is.EqualTo(Frequency.BiAnnual));
        }

        [Test]
        public void ShouldMapMonthlyFrequency()
        {
            var frequencyMapper = new FrequencyMapper();

            Repetition repeat = frequencyMapper.Map(Frequency.Monthly);

            Assert.That(repeat, Is.Not.Null);
            Assert.That(repeat.RecurrenceNumber, Is.EqualTo(1));
            Assert.That(repeat.RecurrenceUnit, Is.EqualTo("Month"));
        }

        [Test]
        public void ShouldMapMonthlyRepetition()
        {
            var frequencyMapper = new FrequencyMapper();

            Frequency frequency = frequencyMapper.Map(new Repetition { RecurrenceNumber = 1, RecurrenceUnit = "Month" });

            Assert.That(frequency, Is.EqualTo(Frequency.Monthly));
        }

        [Test]
        public void ShouldMapQuarterlyFrequency()
        {
            var frequencyMapper = new FrequencyMapper();

            Repetition repeat = frequencyMapper.Map(Frequency.Quarterly);

            Assert.That(repeat, Is.Not.Null);
            Assert.That(repeat.RecurrenceNumber, Is.EqualTo(3));
            Assert.That(repeat.RecurrenceUnit, Is.EqualTo("Month"));
        }

        [Test]
        public void ShouldMapQuarterlyRepetition()
        {
            var frequencyMapper = new FrequencyMapper();

            Frequency frequency = frequencyMapper.Map(new Repetition { RecurrenceNumber = 3, RecurrenceUnit = "Month" });

            Assert.That(frequency, Is.EqualTo(Frequency.Quarterly));
        }

        [Test]
        public void ShouldMapOneTimePayment()
        {
            var frequencyMapper = new FrequencyMapper();

            Repetition repeat = frequencyMapper.Map(Frequency.OneTime);

            Assert.That(repeat, Is.Not.Null);
            Assert.That(repeat.RecurrenceNumber, Is.EqualTo(0));
            Assert.That(repeat.RecurrenceUnit, Is.EqualTo("Day"));
        }

        [Test]
        public void ShouldMapOneTimeRepetition()
        {
            var frequencyMapper = new FrequencyMapper();

            Frequency frequency = frequencyMapper.Map(new Repetition { RecurrenceNumber = 0, RecurrenceUnit = "Day" });

            Assert.That(frequency, Is.EqualTo(Frequency.OneTime));
        }
        
    }
}
