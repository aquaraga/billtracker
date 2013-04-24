using BillTracker.ViewModels;
using NUnit.Framework;
using System.Linq;

namespace BillTracker.Tests.ViewModels
{
    [TestFixture]
    public class BillViewModelTest
    {
        [Test]
        public void ShouldReturnTheListOfPossibleFrequencies()
        {
            var billViewModel = new BillViewModel();

            var selectListItems = billViewModel.FrequencyList.ToList();

            Assert.That(selectListItems.Count, Is.EqualTo(5));

            Assert.That(selectListItems[0].Text, Is.EqualTo("Once a month"));
            Assert.That(selectListItems[1].Text, Is.EqualTo("Once in 3 months"));
            Assert.That(selectListItems[2].Text, Is.EqualTo("Twice a year"));
            Assert.That(selectListItems[3].Text, Is.EqualTo("Once a year"));
            Assert.That(selectListItems[4].Text, Is.EqualTo("One time"));
        }

        [Test]
        public void ShouldGetStringRepresentationOfTheFrequency()
        {
            var billViewModel = new BillViewModel {Frequency = Frequency.BiAnnual};

            Assert.That(billViewModel.FrequencyDisplay, Is.EqualTo("Twice a year"));
        }
    }
}
