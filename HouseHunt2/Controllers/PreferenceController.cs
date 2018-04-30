using HouseHunt2.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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
            
                    preference.Id = User.Identity.GetUserId();
                    //  preference.Prop_Approved = "AWAITING APPROVAL";
                    db.Preferences.Add(preference);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                
               // return RedirectToAction("Create");
         
        }

        // GET: Preference/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Preference/Edit/5
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

        // GET: Preference/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Preference/Delete/5
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
