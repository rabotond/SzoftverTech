using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ReklamServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ServiceReklam : IServiceReklam
    {
        public bool ReklamEmail(string adat)
        {



            MailMessage MyMailMessage = new MailMessage();
            MyMailMessage.From = new MailAddress("nudlimakos@gmail.com");
            MyMailMessage.To.Add("rohodi@hotmail.com");
            MyMailMessage.Subject = "Csillámpóni menhely állatajánlata";
            MyMailMessage.IsBodyHtml = true;

            MyMailMessage.Body = "Az ön számára legmegfelelőbb állatunk örökbeadásra :" + "\r\n" + adat + "\r\n"+ "Amennyiben érdekli ajánlatuk kérjük keressen elérhetőségeinken.";
            SmtpClient SMTPServer = new SmtpClient("smtp.gmail.com");
            SMTPServer.Port = 587;
            SMTPServer.Credentials = new System.Net.NetworkCredential("nudlimakos@gmail.com", "@Jelszo01");
            SMTPServer.EnableSsl = true;

            /* if (!File.Exists(eleres))
              {
                
                  XElement root = new XElement("Gyerekek");
                  XDocument doc = new XDocument(root);
                  XElement gyerek = new XElement("Gyerek",
                   
                        new XElement("nev", textBox1.Text),
                        new XElement("viselkedes", comboBox1.SelectedItem.ToString()),
                        new XElement("nap", DateTime.Now.Date.Day)); 

                  doc.Element("Gyerekek").Add(gyerek);
                  doc.Save(eleres);
             */
            try
            {
                SMTPServer.Send(MyMailMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }
    }
}
