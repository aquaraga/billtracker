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
            //TODO: Map the frequencies
            return new BillViewModel
                       {
                           DueAmount = billModel.DueAmount,
                           End = billModel.End,
                           StartFrom = billModel.StartFrom,
                           Id = billModel.Id,
                           Vendor = billModel.Vendor
                       };
        }
    }
}