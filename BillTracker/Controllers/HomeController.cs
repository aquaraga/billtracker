using System.Web.Mvc;
using BillTracker.Filters;
using WebMatrix.WebData;

namespace BillTracker.Controllers
{
    [InitializeSimpleMembership]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            if (WebSecurity.IsAuthenticated)
                return RedirectToAction("Index", "Bill");

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
