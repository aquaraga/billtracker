using System.Data;
using System.Linq;
using System.Web.Mvc;
using BillTracker.Models;
using BillTracker.ViewModels;
using BillTracker.ViewModels.Mapper;

namespace BillTracker.Controllers
{
    public class BillService : IBillService
    {
        public void SaveBill(BillModel billModel)
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IBillService
    {
        void SaveBill(BillModel billModel);
    }

    public class BillController : Controller
    {
        private readonly IBillModelMapper billModelMapper;
        private readonly IBillService billService;


        private readonly BillContext billContext = new BillContext();

        public BillController(IBillModelMapper billModelMapper, IBillService billService)
        {
            this.billModelMapper = billModelMapper;
            this.billService = billService;
        }

        //
        // GET: /Bill/

        public ActionResult Index()
        {
            return View(billContext.Bills.ToList());
        }

        //
        // GET: /Bill/Details/5

        public ActionResult Details(int id = 0)
        {
            BillModel billmodel = billContext.Bills.Find(id);
            if (billmodel == null)
            {
                return HttpNotFound();
            }
            return View(billmodel);
        }

        //
        // GET: /Bill/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Bill/Create

        [HttpPost]
        public ActionResult Create(BillViewModel billViewModel)
        {
            if (ModelState.IsValid)
            {
                var billModel = billModelMapper.Map(billViewModel);
                billService.SaveBill(billModel);
                return RedirectToAction("Index");
//                billContext.Bills.Add(new BillModel());
//                billContext.SaveChanges();
//                return RedirectToAction("Index");
            }

            return View("");
        }

        //
        // GET: /Bill/Edit/5

        public ActionResult Edit(int id = 0)
        {
            BillModel billmodel = billContext.Bills.Find(id);
            if (billmodel == null)
            {
                return HttpNotFound();
            }
            return View(billmodel);
        }

        //
        // POST: /Bill/Edit/5

        [HttpPost]
        public ActionResult Edit(BillModel billmodel)
        {
            if (ModelState.IsValid)
            {
                billContext.Entry(billmodel).State = EntityState.Modified;
                billContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(billmodel);
        }

        //
        // GET: /Bill/Delete/5

        public ActionResult Delete(int id = 0)
        {
            BillModel billmodel = billContext.Bills.Find(id);
            if (billmodel == null)
            {
                return HttpNotFound();
            }
            return View(billmodel);
        }

        //
        // POST: /Bill/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            BillModel billmodel = billContext.Bills.Find(id);
            billContext.Bills.Remove(billmodel);
            billContext.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            billContext.Dispose();
            base.Dispose(disposing);
        }
    }
}