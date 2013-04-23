using WebMatrix.WebData;

namespace BillTracker.Models
{
    public interface IWebSecurityWrapper
    {
        int GetUserId();
    }

    public class WebSecurityWrapper : IWebSecurityWrapper
    {
        public int GetUserId()
        {
            return WebSecurity.CurrentUserId;
        }
    }
}