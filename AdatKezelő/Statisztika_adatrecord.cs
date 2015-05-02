using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdatKezelő
{
    public class Statisztika_adatrecord
    {
        DateTime nap;
        Guid ugyfelid;

        public Guid Ugyfelid
        {
            get { return ugyfelid; }
            set { ugyfelid = value; }
        }

        public DateTime Nap
        {
            get { return nap; }
            set { nap = value; }
        }

//>>>>>>>>>>>>>>>> napi mozgásokra vonatkozó statisztikai adatok

        public Statisztika_adatrecord(DateTime ujnap)
        {
            nap = ujnap;
        }
        public int hozottAllat { get; set; }
        public int elvittAllat { get; set; }
        public int szabadKennelDarab { get; set; }
        public int befolytEledelAdomany_kg { get; set; }
        public int befolytPenzadomany_huf { get; set; }
        public int regisztraltDarab { get; set; }


//>>>>>>>>>>>>>>>> ugyfelekre vonatkozó statisztikai adatok
        public Statisztika_adatrecord(Guid uju)
        {
            ugyfelid = uju;
        }

        public int eledeltAdomanyozott_kg { get; set; }
        public int pénztAdomanyozott_huf { get; set; }
        public int orokbefogadott_allatai_db { get; set; }
    
    }
}
