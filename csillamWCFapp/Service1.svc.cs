using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace csillamWCFapp
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public void DoWork()
        {
        }

        public string sendEmail(string toaddress, string ujsubject, string ujbody)
        {
           try{
                MailMessage mailMsg = new MailMessage();

                mailMsg.To.Add(new MailAddress(toaddress,"admin"));

                mailMsg.From = new MailAddress("azure_a308ac4719fc68b2129b6ca372127220@azure.com", "menhely ADMIN");

                mailMsg.Subject = ujsubject;
                string text = ujbody;
                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
                SmtpClient smtpClient = new SmtpClient("smtp.sendgrid.net", Convert.ToInt32(587));
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("azure_a308ac4719fc68b2129b6ca372127220@azure.com", "3eNj74ybVPfCd2a");
                smtpClient.Credentials = credentials;

                smtpClient.Send(mailMsg); 
                return "sikeres küldés";
              }
                catch (Exception ex)
              {
                return ex.ToString();

              }
        }
       
    }
}
