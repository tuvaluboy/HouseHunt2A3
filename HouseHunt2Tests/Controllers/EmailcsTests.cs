using NUnit.Framework;
using HouseHunt2.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace HouseHunt2.Controllers.Tests
{
    [TestFixture()]
    public class EmailcsTests
    {

        [Test()]
        public void sendTest()
        {
            
            var cs = new Emailcs();
            var Result = cs.send("fijihomescs415@gmail.com", "cs415assignment", "krishneelkamalsingh@gmail.com", "test", "This is a test");
            
        }
    }
}