using BillTracker.Models;

namespace BillTracker.ViewModels.Mapper
{
    public class BillModelMapper : IBillModelMapper
    {
        private readonly IFrequencyMapper frequencyMapper;

        public BillModelMapper(IFrequencyMapper frequencyMapper)
        {
            this.frequencyMapper = frequencyMapper;
        }

        public BillModel Map(BillViewModel billViewModel, int userId)
        {
            
            return new BillModel
                       {
                           DueAmount = billViewModel.DueAmount,
                           End = billViewModel.End,
                           StartFrom = billViewModel.StartFrom,
                           Vendor = billViewModel.Vendor,
                           UserId = userId,
                           Repeat = frequencyMapper.Map(billViewModel.Frequency)
                       };
        }

        public BillViewModel Map(BillModel billModel)
        {
            return new BillViewModel
                       {
                           DueAmount = billModel.DueAmount,
                           End = billModel.End,
                           StartFrom = billModel.StartFrom,
                           Id = billModel.Id,
                           Vendor = billModel.Vendor,
                           Frequency = frequencyMapper.Map(billModel.Repeat)
                       };
        }

        public void Extend(BillModel billModel, BillViewModel billViewModel)
        {
            billModel.DueAmount = billViewModel.DueAmount;
            billModel.StartFrom = billViewModel.StartFrom;
            billModel.End = billViewModel.End;
            billModel.Repeat = frequencyMapper.Map(billViewModel.Frequency);
        }
    }
}