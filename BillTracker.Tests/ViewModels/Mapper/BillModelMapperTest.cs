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
        private IWebSecurityWrapper webSecurityWrapper;
        private IFrequencyMapper frequencyMapper;
        private BillModelMapper billModelMapper;

        [SetUp]
        public void Setup()
        {
            webSecurityWrapper = MockRepository.GenerateMock<IWebSecurityWrapper>();
            frequencyMapper = MockRepository.GenerateMock<IFrequencyMapper>();
            billModelMapper = new BillModelMapper(webSecurityWrapper, frequencyMapper);
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


            BillModel billModel = billModelMapper.Map(billViewModel);

            Assert.That(billModel.DueAmount, Is.EqualTo(dueAmount));
            Assert.That(billModel.Vendor, Is.EqualTo(vendor));
            Assert.That(billModel.StartFrom, Is.EqualTo(startFrom));
            Assert.That(billModel.End, Is.EqualTo(endTime));
        }

        [Test]
        public void ShouldMapUserId()
        {
            const int userId = 123;
            webSecurityWrapper.Stub(w => w.GetUserId()).Return(userId);

            BillModel billModel = billModelMapper.Map(new BillViewModel());

            Assert.That(billModel.UserId, Is.EqualTo(userId));
        }

        [Test]
        public void ShouldInitiateFrequencyMapping()
        {
            var repeat = new Repetition {RecurrenceNumber = 2, RecurrenceUnit = "Year"};
            frequencyMapper.Stub(f => f.Map(Frequency.BiAnnual)).Return(repeat);
            var billViewModel = new BillViewModel {Frequency = Frequency.BiAnnual};

            BillModel billModel = billModelMapper.Map(billViewModel);

            Assert.That(billModel.Repeat, Is.EqualTo(repeat));
        }
    }

}