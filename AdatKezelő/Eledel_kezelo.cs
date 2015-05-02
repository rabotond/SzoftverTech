using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AdatKezelő.csillamRef;
namespace AdatKezelő
{
    public class Eledel_kezelo // itt lenne háttérszálon az eledel fogyazstás
    {
        Thread t;
        static readonly object lockobj=new object();
        
        System.Timers.Timer timer;
        public Eledel_kezelo()
        {
            t = new Thread(()=> eledelt_fogyaszt());
            timer = new System.Timers.Timer();
            timer.Interval=10000*600;
            timer.Elapsed += timer_Elapsed;
        }

        
        
        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock(lockobj)
            {
                csillamponiDBEntities db = new csillamponiDBEntities();

                var eledelek = db.ELEDEL.Where(x => x.FAJTA != null);
                foreach(ELEDEL ele in eledelek)
                { 
                    
                    if (--ele.RAKTARON<=0)
                    {
                        ele.RAKTARON = 0;
                        Service1Client client = new Service1Client();
                        client.sendEmail("csillamponiproject@gmail.com", ele.FAJTA + " eledele elfogyott", "azonnal kurvagyorsan vegyél nekik kaját");
                        
                      
                    //    Reklam_Hibalog.ServiceReklamClient kliens = new Reklam_Hibalog.ServiceReklamClient();
                     //   kliens.Hibalog("eledele elfogyott azonnal kurvagyorsan vegyél nekik kaját " );
                    
                    }
                }
                db.SaveChanges();
            } 
        }

       public  void eledelt_fogyaszt()
       {
           timer.Start();
       }
    }
}
