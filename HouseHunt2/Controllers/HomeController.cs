using Edi.Web.MVCExtensions.PagedList;
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
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUser user = new ApplicationUser();
        public ActionResult Index()
        {
            ViewBag.Message = "Search for Property";

            return View();
        }

        public ActionResult Agent(string search, int? page)
        {
            ViewBag.Message = "Find An Agent";

     
            //var user = User.Identity;
            //var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            //var s = UserManager.GetRoles(user.GetUserId());
            //if (s[0].ToString() == "Admin")
            //{ }
            var AgentLists = user.Roles.Where(x => x.Equals("Agent")).ToList();
            var users = from u in db.Users
                        where u.Roles.Any(r => r.RoleId == "af7cc10a-ba3d-4b04-be3f-e05707be6a30")
                        select u;
            //return View(UserList.Where(x => x. == search || search == null).ToList().ToPagedList(page ?? 1, 5));
            // return View(db.Roles.Where(x => x.Name.Equals("Agent")).ToList().ToPagedList(page ?? 1, 10));
            return View(users.ToList());

            //    return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "About Us";

            return View();
        }
    }
}