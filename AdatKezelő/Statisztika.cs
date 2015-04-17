using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;


namespace AdatKezelő
{
    public class Statisztika
    {
        private DateTime mettől;
        private DateTime meddig;
        private string tipus;
        List<DateTime> napok;

        public List<DateTime> Napok
        {
            get { return napok; }
            set { napok = value; }
        }

        public DateTime Meddig
        {
            get { return meddig; }
            set { meddig = value; }
        }

        public DateTime Mettől
        {
            get { return mettől; }
            set { mettől = value; }
        }
       
        public string Tipus
        {
            get { return tipus; }
            set { tipus = value; }
        }

        public Statisztika( string ujtipus,DateTime ujmettől,DateTime ujmeddig)
        {
            mettől = ujmettől; meddig = ujmeddig; tipus = ujtipus;
            napok = new List<DateTime>();

            for (int i = 0; i < (meddig - mettől).TotalDays;i++ )
            {
                napok.Add(mettől.AddDays(i));
            }
        }

        public int elvittAllatDb()
        {
            return 0;
        }
        public int hozottAlaltDb()
        {
            return 0;
        }

        public double befolyPenz()
        {
            return 0;
        }

        public double kapottEledel()
        {
            return 0;
        }
        public int szabadKennelDb()
        {
            return 0;
        }

        public int elojegyzesDb()
        {
            return 0;
        }

    }//end Statisztika

}//end namespace AdatKezelő
