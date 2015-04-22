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

        public DateTime Nap
        {
            get { return nap; }
            set { nap = value; }
        }

        public int hozottAllat { get; set; }
        public int elvittAllat { get; set; }
        public int szabadKennelDarab { get; set; }
        public int befolytEledelAdomany { get; set; }
        public int befolytPenzadomany { get; set; }

        public int regisztraltDarab { get; set; }
        public int tamogattaEmenhelyünket { get; set; }

        public Statisztika_adatrecord(DateTime ujnap)
        {
            nap = ujnap;
        }
    }
}
