using HouseHunt2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace HouseHunt2.Controllers
{
    public class Emailcs
    {   
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="password"></param>
        /// <param name="receiver"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        public bool send(string sender, string password, string receiver, string subject, string body)
        {
            string emailSender = sender;
            string emailSenderPassword = password; //cs415assignment
            string emailReceiver = receiver;
            string emailSubject = subject;
            string msgBody = body;

            int aDay = 24;

            //******************************************************
            // DB Stuff get the appointment related table TODO
            HouseHuntEntitiesII db = new HouseHuntEntitiesII();
            var item = from d in db.PropertyAgents
                       select d;

            // *********************************************

            using (var message = new MailMessage(emailSender, emailReceiver))
            {
                foreach (var items in item)
                {

                    // compare current time with appointment time 
                    DateTime inspectionTime = (DateTime)items.AvailableDate;
                    TimeSpan timeGap = inspectionTime.Subtract(DateTime.Now);

                    // ****************************************

                    string note = "Dear Sir \n" +
                        " Please be a advised that you have a house inspection at: " + inspectionTime.ToString();

                    body = note + inspectionTime.ToString();

                    if (timeGap.Hours <= aDay) // if less than or equal to 24 hours and email sent is no
                    {
                        message.Subject = emailSubject;
                        using (SmtpClient client = new SmtpClient
                        {
                            EnableSsl = true,
                            Host = "smtp.gmail.com",
                            Port = 587,
                            Credentials = new NetworkCredential(emailSender, emailSenderPassword)
                        })
                        {
                            try
                            {
                                client.Send(message);
                            }
                            catch (SqlException e)
                            {
                                throw new Exception("Error" + e);
                            }    
                            
                            
                            
                        }

                    }


                }
            }
            return true;
        }


    }
}