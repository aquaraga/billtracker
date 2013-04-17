using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BillTracker.Models;

namespace BillTracker.Controllers
{
    public class BillController : Controller
    {
        private readonly BillContext db = new BillContext();

        //
        // GET: /Bill/

        public ActionResult Index()
        {
            return View(db.Bills.ToList());
        }

        //
        // GET: /Bill/Details/5

        public ActionResult Details(int id = 0)
        {
            BillModel billmodel = db.Bills.Find(id);
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
        public ActionResult Create(BillModel billmodel)
        {
            if (ModelState.IsValid)
            {
                db.Bills.Add(billmodel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(billmodel);
        }

        //
        // GET: /Bill/Edit/5

        public ActionResult Edit(int id = 0)
        {
            BillModel billmodel = db.Bills.Find(id);
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
                db.Entry(billmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(billmodel);
        }

        //
        // GET: /Bill/Delete/5

        public ActionResult Delete(int id = 0)
        {
            BillModel billmodel = db.Bills.Find(id);
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
            BillModel billmodel = db.Bills.Find(id);
            db.Bills.Remove(billmodel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}