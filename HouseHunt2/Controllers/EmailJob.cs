using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Configuration;
using HouseHunt2.Models;
using System.Data.SqlClient;

namespace HouseHunt2.Controllers
{
    public class EmailJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            // Email Stuff
            string sender = "fijihomescs415@gmail.com";
            string senderPassword = "cs415assignment";
            string receiver = "krishneelkamalsingh@gmail.com";
            string msgSubject = "Inspection Of Property";
            string msgBody = "Hello";


            //******************************************************
            // DB Stuff get the appointment related table TODO
            HouseHountEntities db = new HouseHountEntities();
            var item = from d in db.PropertyAgents
                       select d;


            // **********************************************


            // *********************************************

            using (var message = new MailMessage(sender, receiver))
            {


                foreach (var items in item)
                {

                    // compare current time with appointment time 
                    string date = items.AvailableDate.ToString();
                    DateTime startTime = DateTime.Now;
                    DateTime endTime = (DateTime)items.AvailableDate;
                    TimeSpan span = endTime.Subtract(startTime);


                    // ****************************************

                    string note = "Dear Sir \n" +
                        " Please be a advised that you have a house inspection at: " + items.AvailableDate.ToString();

                    string dateapp = items.AvailableDate.ToString();

                    msgBody = "";

                    if (span.Hours <= 24) // if less than or equal to 24 hours and email sent is no
                    {
                        message.Subject = msgSubject;
                        message.Body = note;
                        using (SmtpClient client = new SmtpClient
                        {
                            EnableSsl = true,
                            Host = "smtp.gmail.com",
                            Port = 587,
                            Credentials = new NetworkCredential(sender, senderPassword)
                        })
                        {
                            client.Send(message);
                        }

                    }


                }
            }
        }


    }
}