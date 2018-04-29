using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HouseHunt2.Models;
using Edi.Web.MVCExtensions.PagedList;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HouseHunt2.Controllers
{
    public class HHModelController : Controller
    {
        private HHDBEntities db = new HHDBEntities();

        //public HttpPostedFileBase image1 { get; private set; }

        #region Owner
        // GET: HHModel/Owner
        public ActionResult OwnerIndex()
        {
            return View(db.Owners.ToList());
        }

        // GET: HHModel/CreateOwner
        public ActionResult OwnerCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OwnerCreate([Bind(Include = "Owner_Id, Own_First, Own_Last, Own_Gender, Own_DOB, Own_Street, Own_City, Own_Phone, Own_Email ")] Owner owner)
        {
            if (ModelState.IsValid)
            {
                db.Owners.Add(owner);
                db.SaveChanges();
                return RedirectToAction("OwnerIndex");
            }

            return View(owner);
        }
        #endregion

        #region Agent
        // GET: HHModel/Agent
        public ActionResult AgentIndex()
        {
            return View(db.Agents.ToList());
        }

        // GET: HHModel/CreateAgent
        public ActionResult AgentCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgentCreate([Bind(Include = "Ag_Id, Ag_First, Ag_Last, Ag_Gender, Ag_DOB, Ag_Street, Ag_City, Ag_Phone, Ag_Email ")] Agent agent)
        {
            if (ModelState.IsValid)
            {
                db.Agents.Add(agent);
                db.SaveChanges();
                return RedirectToAction("AgentIndex");
            }

            return View(agent);
        }
        #endregion

        #region Property
        // GET: HHModel/Properties
        public ActionResult Index(string searchBy, string search, int? page)
        {
            //return View(db.Properties.ToList());
            if (searchBy == "Location")
            {
                return View(db.Properties.Where(x => x.Prop_City == search || search == null).ToList().ToPagedList(page ?? 1, 5));
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
                return View(db.Properties.Where(x => x.Prop_Price == nvalue || search == null).ToList().ToPagedList(page ?? 1, 5));
            }
        

    }


        public ActionResult PropIndex(string searchBy, string search, int? page)
        {
            //return View(db.Properties.ToList());
            var item = (from i in db.Properties
                        select i).ToList();

            ViewBag.item = item;
            //  return View(db.Properties.Where(x => x.Prop_City.Equals(1)).ToList().ToPagedList(page ?? 1, 10));
            if (searchBy == "Location") {
                return View(db.Properties.Where(x => x.Prop_City == search || search == null).ToList().ToPagedList(page ?? 1, 5));
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
                return View(db.Properties.Where(x => x.Prop_Price == nvalue || search == null).ToList().ToPagedList(page ?? 1, 5));
            }
        }

        // GET: HHModel/CreateProperties
        public ActionResult PropCreate()
        {
            //var ownerlist = from s in db.Owners
            //                select s;
            //var agentlist = from d in db.Agents
            //                select d;
            //ViewBag.OwnerList = ownerlist.ToList();
            //ViewBag.AgentList = agentlist.ToList();
            //ViewBag.Owners = new SelectList(db.Owners, "Owner_Id", "Own_First");
            //ViewBag.Agents = new SelectList(db.Agents, "Ag_Id", "Ag_First");
            //List<> owner = ownerlist.ToList();

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PropCreate(HttpPostedFileBase image1, [Bind(Include = "Prop_Id, Prop_Street, Prop_City, Prop_Avail, Prop_Condition, Prop_Price, Owner_Id, Ag_Id, Prop_Img")] Property property)
        {
            if (ModelState.IsValid)
            {
                if (image1 != null)
                {
                    property.Prop_Img = new byte[image1.ContentLength];
                    image1.InputStream.Read(property.Prop_Img, 0, image1.ContentLength);

                }

                
                db.Properties.Add(property);
                db.SaveChanges();
                return RedirectToAction("PropIndex");
            }

            return View(property);
        }
        #endregion

        #region CreateCustomer

        // GET: HHModel/Customer
        public ActionResult CustomerDetails()
        {
            return View(db.Customers.ToList());
        }
        // GET: HHModel/Create
        public ActionResult CusCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CusCreate([Bind(Include = "Cus_Id,Cus_First,Cus_Last,Cus_Gender,Cus_DOB,Cus_Street,Cus_City,Cus_Phone,Cus_Email")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("CustomerDetails");
            }

            return View(customer);
        }
        #endregion

        #region Booking

        // public ActionResult CreateBook(int? id)
        // {
        //     if (id == null)
        //     {
        //         return RedirectToAction("Index", "HHModel");
        //         //  return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //     }
        ////     Boxer Boxer = context.Boxer.Find(id);
        //     var prop = db.Properties.Find(id);
        //     if (prop == null)
        //     {
        //         return RedirectToAction("Index", "Home");
        //         //  return HttpNotFound();
        //     }
        //     return View(prop);
        //     // return View();
        // }
        #endregion

        #region Image
        //public ActionResult Img()
        //{

        //    var item = (from i in db.Properties
        //                select i).ToList();

        //    return View(item);
        //}

        //public ActionResult AddImg()
        //{
        //    Property b1 = new Property();

        //    var ownerlist = from s in db.Owners
        //                    select s;
        //    var agentlist = from d in db.Agents
        //                    select d;
        //    ViewBag.OwnerList = ownerlist.ToList();
        //    ViewBag.AgentList = agentlist.ToList();
        //    ViewBag.Owners = new SelectList(db.Owners, "Owner_Id", "Own_First");
        //    ViewBag.Agents = new SelectList(db.Agents, "Ag_Id", "Ag_First");

        //    return View();
        //}

        //[HttpPost]
        //public ActionResult AddImg(Property model, HttpPostedFileBase image1)
        //{
        //    if (image1 != null)
        //    {
        //        model.Prop_Img = new byte[image1.ContentLength];
        //        image1.InputStream.Read(model.Prop_Img, 0, image1.ContentLength);

        //    }

        //    db.Properties.Add(model);
        //    db.SaveChanges();

        //    return RedirectToAction("AddImg");
        //    return View(model);
        //}
        #endregion

        #region Admin and Agent
        public Boolean isAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
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

        /*
        // GET: HHModel/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HHModel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HHModel/Create
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

        // GET: HHModel/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HHModel/Edit/5
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

        // GET: HHModel/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HHModel/Delete/5
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
        }*/
    }
}
