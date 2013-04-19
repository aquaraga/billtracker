using BillTracker.Models;

namespace BillTracker.ViewModels.Mapper
{
    public class BillModelMapper : IBillModelMapper
    {
        private readonly IWebSecurityWrapper webSecurityWrapper;
        private readonly IFrequencyMapper frequencyMapper;

        public BillModelMapper(IWebSecurityWrapper webSecurityWrapper, IFrequencyMapper frequencyMapper)
        {
            this.webSecurityWrapper = webSecurityWrapper;
            this.frequencyMapper = frequencyMapper;
        }

        public BillModel Map(BillViewModel billViewModel)
        {
            
            return new BillModel
                       {
                           DueAmount = billViewModel.DueAmount,
                           End = billViewModel.End,
                           StartFrom = billViewModel.StartFrom,
                           Vendor = billViewModel.Vendor,
                           UserId = webSecurityWrapper.GetUserId(),
                           Repeat = frequencyMapper.Map(billViewModel.Frequency)
                       };
        }
    }
}