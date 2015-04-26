using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdatKezelő
{
    public class Eledel_kezelo // itt lenne háttérszálon az eledel fogyazstás
    {
        Thread t;
        csillamponimenhelyDBEntities db;
        System.Timers.Timer timer;
        public Eledel_kezelo()
        {
            t = new Thread(()=> eledelt_fogyaszt());
            db = new csillamponimenhelyDBEntities();
            timer = new System.Timers.Timer();
            timer.Interval=1000;
            timer.Elapsed += timer_Elapsed;
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            
        }

       public  void eledelt_fogyaszt()
       {
           timer.Start();
       }
    }
}
