using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using BillTracker.Filters;
using BillTracker.Models;
using BillTracker.Services;
using BillTracker.ViewModels;
using BillTracker.ViewModels.Mapper;

namespace BillTracker.Controllers
{
    [InitializeSimpleMembership]
    public class BillController : Controller
    {
        private readonly IBillModelMapper billModelMapper;
        private readonly IBillService billService;
        private readonly IWebSecurityWrapper webSecurityWrapper;


        public BillController(IBillModelMapper billModelMapper, IBillService billService, IWebSecurityWrapper webSecurityWrapper)
        {
            this.billModelMapper = billModelMapper;
            this.billService = billService;
            this.webSecurityWrapper = webSecurityWrapper;
        }

        //
        // GET: /Bill/

        public ActionResult Index()
        {
            int userId = webSecurityWrapper.GetUserId();
            IEnumerable<BillModel> billsForUser = billService.GetBillsForUser(userId);
            IEnumerable<BillViewModel> billViewModels = billsForUser.Select(b => billModelMapper.Map(b));
            return View(billViewModels.ToList());
        }

        //
        // GET: /Bill/Details/5

        public ActionResult Details(int id = 0)
        {
            BillModel billModel = billService.GetBill(id);
            BillViewModel billViewModel = billModelMapper.Map(billModel);
            return View(billViewModel);
        }

        //
        // GET: /Bill/Create

        public ActionResult Create()
        {
            return View(new BillViewModel());
        }

        //
        // POST: /Bill/Create

        [HttpPost]
        public ActionResult Create(BillViewModel billViewModel)
        {
            if (ModelState.IsValid)
            {
                var billModel = billModelMapper.Map(billViewModel, webSecurityWrapper.GetUserId());
                billService.SaveBill(billModel);
                return RedirectToAction("Index");
            }

            return View("Create", billViewModel);
        }

        //
        // GET: /Bill/Edit/5

        public ActionResult Edit(int id = 0)
        {
            BillModel billModel = billService.GetBill(id);
            BillViewModel billViewModel = billModelMapper.Map(billModel);
            return View(billViewModel);
        }

        //
        // POST: /Bill/Edit/5

        [HttpPost]
        public ActionResult Edit(BillViewModel billViewModel)
        {
            if (ModelState.IsValid)
            {
                BillModel billModel = billService.GetBill(billViewModel.Id);
                billModelMapper.Extend(billModel, billViewModel);
                billService.ModifyBill(billModel);
                return RedirectToAction("Index");
            }
            return View(billViewModel);
        }

        //
        // GET: /Bill/Delete/5

        public ActionResult Delete(int id)
        {
            BillModel billModel = billService.GetBill(id);
            BillViewModel billViewModel = billModelMapper.Map(billModel);
            return View(billViewModel);
        }

        //
        // POST: /Bill/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            billService.DeleteBill(id);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            billService.Dispose();
            base.Dispose(disposing);
        }
    }
}