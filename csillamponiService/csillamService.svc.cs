using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace csillamponiService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "csillamService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select csillamService.svc or csillamService.svc.cs at the Solution Explorer and start debugging.
    public class csillamService : IcsillamService
    {
        public void DoWork()
        {
        }


        public void sendEmail(string to_address, string ujsubject, string message)
        {
            var fromAddress = new MailAddress("csillamponiproject@gmail.com", "csillám_menhely_error_sender");
            var toAddress = new MailAddress(to_address, "admin");
            const string fromPassword = "csillamponi";
             string subject = ujsubject;
             string body = message;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var email = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(email);
            }
        }
    }
}
