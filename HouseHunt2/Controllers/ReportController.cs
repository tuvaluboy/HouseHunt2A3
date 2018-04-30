using System;
using HouseHunt2.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseHunt2.Controllers
{
    public class ReportController : Controller
    {
        private HouseHuntEntitiesII db = new HouseHuntEntitiesII();
        private ApplicationDbContext context = new ApplicationDbContext();

        // GET: Report
        public ActionResult ReportIndex()
        {

            return View(db.Reports.ToList());
        }

        // GET: Report/Details/5
        public ActionResult ReportDetails([Bind(Include = "Report_Id,Prop_Id,Prop_Date,Prop_Street,Prop_City,Id,Prop_Price,Prop_Bond")] Report report)
        {
            db.Reports.Add(report);
            db.SaveChanges();

            return RedirectToAction("ReportIndex");
        }

        // GET: Report/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Report/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Report/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Report/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Report/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Report/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
