using NUnit.Framework;
using HouseHunt2.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseHunt2.Models;
using System.Web.Mvc;

namespace HouseHunt2.Controllers.Tests
{
    [TestFixture()]
    public class PreferenceControllerTests
    {
        /// <summary>
        /// Test the method that creates a new user preference
        /// </summary>
        [Test()]
        public void CreateTest1()
        {


            Assert.Fail();
        }

        [Test()]
        public void EditTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void EditTest1()
        {
            Assert.Fail();
        }

        [Test()]
        public void DeleteTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void DeleteTest1()
        {
            Assert.Fail();
        }

        [Test()]
        public void CreateTest()
        {
            //PreferenceController precon = new PreferenceController();

            //Preference preference = new Preference();
            //preference.Bedroom = 2;
            //preference.Location = "Nasinu";
            //preference.Area = "Caqiri";
            //preference.Price = 300;
            //preference.Baths = 2;
            //preference.NearbyLocation = "LTA Valelevu";


            //ActionResult result = precon.Create(preference);
            //var preferenceobje = (Preference)result.ViewData.Model;

            //Assert.IsInstanceOf(result, typeof(RedirectToRouteResult));
            //RedirectToRouteResult routeResult = result as RedirectToRouteResult;
            //Assert.AreEqual(routeResult.RouteValues["action"], "Index");
        }

        [Test()]
        public void IndexTest()
        {
            var precon = new PreferenceController();

            Preference preference = new Preference();
            // var result = precon.Index
            Assert.Fail();
        }
    }
}      