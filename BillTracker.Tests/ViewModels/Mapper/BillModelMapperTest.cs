using System;
using BillTracker.Models;
using BillTracker.ViewModels;
using BillTracker.ViewModels.Mapper;
using NUnit.Framework;
using Rhino.Mocks;

namespace BillTracker.Tests.ViewModels.Mapper
{
    [TestFixture]
    public class BillModelMapperTest
    {
        private IFrequencyMapper frequencyMapper;
        private BillModelMapper billModelMapper;

        [SetUp]
        public void Setup()
        {
            frequencyMapper = MockRepository.GenerateMock<IFrequencyMapper>();
            billModelMapper = new BillModelMapper(frequencyMapper);
        }

        [Test]
        public void ShouldMapABillViewModel()
        {
            const decimal dueAmount = 230m;
            const string vendor = "Airtel";
            var endTime = new DateTime(2014, 01, 11);
            var startFrom = new DateTime(2012, 02, 03);
            var billViewModel = new BillViewModel
                                    {
                                        Vendor = vendor,
                                        DueAmount = dueAmount,
                                        End = endTime,
                                        StartFrom = startFrom,
                                        Frequency = Frequency.BiAnnual
                                    };


            const int userId = 123;
            BillModel billModel = billModelMapper.Map(billViewModel, userId);

            Assert.That(billModel.DueAmount, Is.EqualTo(dueAmount));
            Assert.That(billModel.Vendor, Is.EqualTo(vendor));
            Assert.That(billModel.StartFrom, Is.EqualTo(startFrom));
            Assert.That(billModel.End, Is.EqualTo(endTime));
            Assert.That(billModel.UserId, Is.EqualTo(userId));
        }

        [Test]
        public void ShouldInitiateFrequencyMapping()
        {
            var repeat = new Repetition {RecurrenceNumber = 6, RecurrenceUnit = "Month"};
            frequencyMapper.Stub(f => f.Map(Frequency.BiAnnual)).Return(repeat);
            var billViewModel = new BillViewModel {Frequency = Frequency.BiAnnual};

            BillModel billModel = billModelMapper.Map(billViewModel, 0);

            Assert.That(billModel.Repeat, Is.EqualTo(repeat));
        }

        [Test]
        public void ShouldMapABillModelToItsViewModel()
        {
            var repetition = new Repetition {RecurrenceNumber = 1, RecurrenceUnit = "Year"};
            frequencyMapper.Stub(f => f.Map(repetition)).Return(Frequency.Annual);
            var billModel = new BillModel
                                 {
                                     DueAmount = 25.5m, 
                                     UserId = 123,
                                     StartFrom = DateTime.Today,
                                     End = DateTime.Today.AddYears(5),
                                     Repeat = repetition,
                                     ExpenseType = new ExpenseType(),
                                     Id = 1234,
                                     Vendor = "Airtel"
                                 };
            BillViewModel billViewModel = billModelMapper.Map(billModel);

            Assert.That(billViewModel.DueAmount, Is.EqualTo(25.5m));
            Assert.That(billViewModel.Frequency, Is.EqualTo(Frequency.Annual));
        }

        [Test]
        public void ShouldExtendAModelBasedOnUserInputs()
        {   
            var billModel = new BillModel
            {
                DueAmount = 25.5m,
                UserId = 123,
                StartFrom = DateTime.Today,
                End = DateTime.Today.AddYears(5),
                Repeat = new Repetition { RecurrenceNumber = 1, RecurrenceUnit = "Year" },
                ExpenseType = new ExpenseType(),
                Id = 1234,
                Vendor = "Airtel"
            };

            const decimal newDueAmount = 50m;
            DateTime newEndDate = DateTime.Today.AddMonths(10);
            DateTime newStartDate = DateTime.Today.AddDays(4);
            const Frequency newFrequency = Frequency.BiAnnual;
            var repeat = new Repetition { RecurrenceNumber = 6, RecurrenceUnit = "Month" };
            frequencyMapper.Stub(f => f.Map(Frequency.BiAnnual)).Return(repeat);
            var billViewModel = new BillViewModel
            {
                DueAmount = newDueAmount,
                End = newEndDate,
                StartFrom = newStartDate,
                Frequency = newFrequency,
                Vendor = "SHOULD NOT CHANGE"
            };

            billModelMapper.Extend(billModel, billViewModel);

            Assert.That(billModel.Vendor, Is.EqualTo("Airtel"));
            Assert.That(billModel.DueAmount, Is.EqualTo(newDueAmount));
            Assert.That(billModel.End, Is.EqualTo(newEndDate));
            Assert.That(billModel.StartFrom, Is.EqualTo(newStartDate));
            Assert.That(billModel.Repeat.RecurrenceNumber, Is.EqualTo(6));
            Assert.That(billModel.Repeat.RecurrenceUnit, Is.EqualTo("Month"));
        }
    }

}