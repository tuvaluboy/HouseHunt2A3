using HouseHunt2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseHunt2.Controllers
{
    public class UserRoleController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

       
        // GET: UserRole
        public ActionResult UserRoleIndex()
        {

            var userid = (from s in context.Users
                          select s).ToList();
            if (User.Identity.IsAuthenticated)
            {


                if (!isAdminUser())
                {
                    return RedirectToAction("Index", "HHModel");
                }
            }
            else
            {
                return RedirectToAction("Index", "HHModel");
            }
            ViewBag.userList = new SelectList(context.Users, "Id", "Email"); ;
            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            //var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
           
            return View();
        }
        [HttpPost]
        public ActionResult UserRoleIndex(FormCollection Fc)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            try
            {
                var user = new ApplicationUser();

                user.Id = Fc["Id"].ToString();

                var result1 = UserManager.AddToRole(user.Id, "Agent");

                // TODO: Add insert logic here

                return RedirectToAction("UserRoleIndex");
            }
            catch
            {
                return View();
            }
        }
        // GET: UserRole/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserRole/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserRole/Create
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

        // GET: UserRole/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserRole/Edit/5
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

        // GET: UserRole/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserRole/Delete/5
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
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
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
    }
}
