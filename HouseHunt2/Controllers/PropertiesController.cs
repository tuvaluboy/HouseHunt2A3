using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HouseHunt2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Edi.Web.MVCExtensions.PagedList;
using System.Data.Entity.Infrastructure;

namespace HouseHunt2.Controllers
{
    public class PropertiesController : Controller
    {
        private HouseHountEntities db = new HouseHountEntities();
        private ApplicationDbContext context = new ApplicationDbContext();

        #region Admin and Agent
        public Boolean isAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {

                var user = User.Identity;
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Admin")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public Boolean isAgentUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Agent")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }


        #endregion

        // GET: Properties
        public ActionResult PropertyIndex(string search, string searchBy, int? page)
        {
            //var properties = db.Properties.Include(p => p.AspNetUser);
            var item = from d in db.Properties
                       where d.Prop_Approved == "APPROVED"
                       select d;
          
            if (searchBy == "Location")
            {
                return View(item.Where(x => x.Prop_City == search || search == null).ToList().ToPagedList(page ?? 1, 5));
            }
            else if(searchBy=="Price")
            {
                int nvalue;
                try
                {
                    nvalue = Convert.ToInt32(search);
                }
                catch (Exception)
                {

                    throw;
                }
                return View(item.Where(x => x.Prop_Price == nvalue || search == null).ToList().ToPagedList(page ?? 1, 5));


                //  return View(item.ToList().ToPagedList(page ?? 1, 5));
            }
            else if(searchBy == "Rooms")
            {
                int nvalue;
                try
                {
                    nvalue = Convert.ToInt32(search);
                }
                catch (Exception)
                {

                    throw;
                }
                return View(item.Where(x => x.Prop_NumOfBed == nvalue || search == null).ToList().ToPagedList(page ?? 1, 5));

            }
            else
            {
                int nvalue;
                try
                {
                    nvalue = Convert.ToInt32(search);
                }
                catch (Exception)
                {

                    throw;
                }
                return View(item.Where(x => x.Prop_NumOfBath == nvalue || search == null).ToList().ToPagedList(page ?? 1, 5));

            }
        }

        public ActionResult PropertyMy()
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("PropertyIndex");
            }
            //var properties = db.Properties.Include(p => p.AspNetUser);
            string user = User.Identity.GetUserId().ToString();
            var item = from d in db.Properties
                       where d.Id == user
                       select d;

            return View(item.ToList());
        }

        public ActionResult PropertyManager()
        {

            if (User.Identity.IsAuthenticated)
            {
                if (!isAdminUser())
                {
                    return RedirectToAction("PropertyIndex");
                }

            }
            else
            {
                return RedirectToAction("PropertyIndex");
            }
            //var properties = db.Properties.Include(p => p.AspNetUser);
            string user = User.Identity.GetUserId().ToString();
            var item = from d in db.Properties
                       select d;

            return View(item.ToList());
        }

        public ActionResult PropertyApprove(int? id)
        {


            var users = from u in context.Users
                        where u.Roles.Any(r => r.RoleId == "af7cc10a-ba3d-4b04-be3f-e05707be6a30")
                        select u;


            ViewBag.Agents = new SelectList(users, "Id", "FirstName");

            var property = from q in db.Properties
                           where q.Prop_Id == id
                           select q;

            return View(property);
        }

        // POST: HHModel/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PropertyApprove(FormCollection id, [Bind(Include = "Id")] PropertyAgent propertyagent)
        {
            propertyagent.Prop_Id = Convert.ToInt32(id["Prop_Id"]);
            db.PropertyAgents.Add(propertyagent);
            db.SaveChanges();

            return RedirectToAction("Edit", "Properties", new { id = propertyagent.Prop_Id });
        }

        #region AgentProperty




        #endregion


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var users = from u in context.Users
                        where u.Roles.Any(r => r.RoleId == "af7cc10a-ba3d-4b04-be3f-e05707be6a30")
                        select u;


            ViewBag.Agents = new SelectList(users, "Id", "FirstName");


            var property = from q in db.Properties
                           where q.Prop_Id == id
                           select q;
            Property properties = db.Properties.Find(id);
            return View(properties);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var PropertiesToUpdate = db.Properties.Find(id);
            if (TryUpdateModel(PropertiesToUpdate, "",
               new string[] { "Prop_Approved" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("PropertyManager");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            //   PopulateDepartmentsDropDownList(courseToUpdate.DepartmentID);
            return View(PropertiesToUpdate);
        }

        // GET: Properties/Create
        public ActionResult PropertyCreate()
        {
            // ViewBag.Id = new SelectList( , "Id", "FirstName");
            ViewBag.Condition = new SelectList("Fully Furnished", "Partial Furnished");

            return View();
        }

        // POST: Properties/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PropertyCreate(HttpPostedFileBase image1, [Bind(Include = "Prop_Id,Prop_Street,Prop_City,Prop_NumOfBed,Prop_NumOfBath,Prop_Condition,Prop_Price,Prop_Description,Prop_Approved,Id,Prop_Img")] Property property)
        {
            if (ModelState.IsValid)
            {
                if (image1 != null)
                {
                    property.Prop_Img = new byte[image1.ContentLength];
                    image1.InputStream.Read(property.Prop_Img, 0, image1.ContentLength);

                }

                property.Id = User.Identity.GetUserId();
                property.Prop_Approved = "AWAITING APPROVAL";
                db.Properties.Add(property);
                db.SaveChanges();
                return RedirectToAction("PropertyIndex");
            }

            //ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "FirstName", property.Id);
            return View(property);
        }

        // GET: Property Details
        public ActionResult PropertyDetails(int? id)
        {
            var item = from d in db.Properties
                       where d.Prop_Id == id
                       select d;
            var iteminAP = from z in db.PropertyAgents
                           where z.Prop_Id == id
                           select z.AvailableDate;

            ViewBag.IteminAp = iteminAP;

            var dates = from s in db.PropertyAgents
                        where s.Prop_Id == id
                        select s;
           // ViewBag.datelist = new SelectList(dates, "PAId", "AvailableDate");
            return View(item);
        }

        public ActionResult BookInspection()
        {
            //var item = from d in db.Properties
            //           where d.Prop_Id == id
            //           select d;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PropertyBook(FormCollection item)
        {
            
            return View();
        }



    }
}
