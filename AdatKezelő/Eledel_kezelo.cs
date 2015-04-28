using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
            timer.Interval=1000;
            timer.Elapsed += timer_Elapsed;
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock(lockobj)
            {
                csillamponiDBEntities db = new csillamponiDBEntities();
                Debug.WriteLine("cicacica" + DateTime.Now);
                ELEDEL ele = db.ELEDEL.Where(x => x.FAJTA == "wombat").FirstOrDefault();
                if (--ele.RAKTARON==0)
                {
                    csillamService.IcsillamServiceClient client = new csillamService.IcsillamServiceClient();
                    client.SendMail("a.lynxie@gmail.com","eledel elfogyott", "azonnal kurvagyorsan vegyél wombiknak kaját");
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
