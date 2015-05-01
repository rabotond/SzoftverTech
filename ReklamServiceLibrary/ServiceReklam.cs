using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml.Linq;

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

            MyMailMessage.Body = "Az ön számára legmegfelelőbb állatunk örökbeadásra :" + "\r\n" + adat + "\r\n" + "Amennyiben érdekli ajánlatuk kérjük keressen elérhetőségeinken.";
            SmtpClient SMTPServer = new SmtpClient("smtp.gmail.com");
            SMTPServer.Port = 587;
            SMTPServer.Credentials = new System.Net.NetworkCredential("nudlimakos@gmail.com", "@Jelszo01");
            SMTPServer.EnableSsl = true;

            try
            {
                SMTPServer.Send(MyMailMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                Hibalog(ex.Message);
            }

        }
        public bool Hibalog(string adat)
        {
            try
            {

                if (File.Exists("Hibalog.xml"))
                {

                    XDocument doc = XDocument.Load("Hibalog.xml");
                    XElement tel = new XElement("hiba",
                         new XElement("hiba_megnevezes", adat),
                         new XElement("nap", DateTime.Now));
                    doc.Element("Hibak").Add(tel);
                    doc.Save("Hibalog.xml");

                    return true;
                }
                else
                {
                    XElement root = new XElement("Hibak");
                    XDocument doc = new XDocument(root);
                    doc.Save("Hibalog.xml");
                    XDocument doc2 = XDocument.Load("Hibalog.xml");
                    XElement tel = new XElement("hiba",
                         new XElement("hiba_megnevezes", adat),
                         new XElement("nap", DateTime.Now));
                    doc2.Element("Hibak").Add(tel);
                    doc2.Save("Hibalog.xml");
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
                
            }


        }
    }
}
    

