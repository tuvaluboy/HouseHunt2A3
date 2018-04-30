using HouseHunt2.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HouseHunt2.Controllers
{
    public class PreferenceController : Controller
    {
        private HouseHuntEntitiesII db = new HouseHuntEntitiesII();
        // GET: Preference
        public ActionResult Index()
        {
            var userid = User.Identity.GetUserId();
            var item = from d in db.Preferences
                       where d.Id == userid
                       select d;


            return View(item.ToList());
        }

        // GET: Preference/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Preference/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Preference/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PreId,Bedroom,Location,Area,Price,Baths,NearbyLocation")] Preference preference)
        {
            int value = 1;

            if (ModelState.IsValid)
            {
                preference.Id = User.Identity.GetUserId();
                //  preference.Prop_Approved = "AWAITING APPROVAL";
                db.Preferences.Add(preference);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("Create");
                
               // return RedirectToAction("Create");
         
        }

        // GET: Preference/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Preference preference = db.Preferences.Find(id);
            if (preference == null)
            {
                return HttpNotFound();
            }
            return View(preference);
        }

        // POST: Preference/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult Edit([Bind(Include = "PreId,Bedroom,Location,Area,Price,Baths,NearbyLocation")] Preference preference)
        {
            if (ModelState.IsValid)
            {
                db.Entry(preference).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(preference);
        }

        // GET: Preference/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Preference preference = db.Preferences.Find(id);
            if ( preference == null)
            {
                return HttpNotFound();
            }
            return View(preference);
        }

        // POST: Preference/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            Preference preference = db.Preferences.Find(id);
            db.Preferences.Remove(preference);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
