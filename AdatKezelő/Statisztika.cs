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
        private Statisztika_típus tipus;
        List<DateTime> napok;
        csillamponimenhelyDBEntities db;
        List<int> elvittAllatlista;
        List<int> hozottAllatlista;
        List<int> befolyPenzlista;
        List<int> kapottEledellista;
        List<int> szabadKennelek;


        public List<List<int>> MakeStatistic()
        {
            List<List<int>> listaklistaja=new List<List<int>>();

            if(tipus==Statisztika_típus.állatállomány)
            {
                listaklistaja.Add( elvittAllatlista = elvittAllatDb());
                listaklistaja.Add(hozottAllatlista = hozottAllatDb());
                listaklistaja.Add(szabadKennelek = szabadKennelDb());
                return listaklistaja;
            }
            else if(tipus==Statisztika_típus.adomány)
            {
                listaklistaja.Add(befolyPenzlista = befolyPenz());
                listaklistaja.Add( kapottEledellista = kapottEledel());
                return listaklistaja;
            }
            else
            {
               listaklistaja.Add( elvittAllatlista = elvittAllatDb());
               listaklistaja.Add(  hozottAllatlista = hozottAllatDb());
               listaklistaja.Add(  szabadKennelek = szabadKennelDb());
               listaklistaja.Add(befolyPenzlista = befolyPenz());
               listaklistaja.Add(kapottEledellista = kapottEledel());
               return listaklistaja;
            }
            
        }

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

        public Statisztika(Statisztika_típus ujtipus, DateTime ujmettől, DateTime ujmeddig)
        {
            mettől = ujmettől; meddig = ujmeddig; tipus = ujtipus;
            napok = new List<DateTime>();
            db = new csillamponimenhelyDBEntities();

            for (int i = 0; i < (meddig - mettől).TotalDays;i++ )
            {
                napok.Add(mettől.AddDays(i));
            }
        }

        public List<int> elvittAllatDb()
        {
            List<int> list = new List<int>();
            foreach(DateTime akt in napok)
            {
                list.Add(db.ALLAT.Count(x => x.OROKBEFOGADVA==akt));
            }
            return list;
        }
        public List<int> hozottAllatDb()
        {
            List<int> list = new List<int>();
            foreach (DateTime akt in napok)
            {
                list.Add(db.ALLAT.Count(x => x.BEADVA == akt));
            }
            return list;
        }

        public List<int> befolyPenz()
        {
            List<int> list = new List<int>();
            foreach (DateTime akt in napok)
            {  
                list.Add( (int)db.ADOMANY.Where(x => x.DATUM == akt && x.TIPUS == "pénz").Sum(x => x.MENNYISEG));
            }
            return list;
        }

        public List<int> kapottEledel()
        {
            List<int> list = new List<int>();
            foreach (DateTime akt in napok)
            {
                list.Add((int)db.ADOMANY.Where(x => x.DATUM == akt && x.TIPUS == "eledel").Sum(x => x.MENNYISEG));
            }
            return list;
        }
        public List<int> szabadKennelDb()
        {
            List<int> list = new List<int>();
            int aznapiAllatDb = 0;
            int szabadkennelDb = 0;
            foreach (DateTime akt in napok)
            {
                szabadkennelDb = db.KENNEL.Sum(x=>x.MAXDARAB);
                aznapiAllatDb = db.ALLAT.Count(x=>x.BEADVA<akt && x.OROKBEFOGADVA>akt);
                list.Add(szabadkennelDb-aznapiAllatDb);
            }
            return list;
        }

        //public List<int> elojegyzesDb()
        //{
        //    List<int> list = new List<int>();
        //    //foreach (DateTime akt in napok)
        //    //{
        //    //    list.Add((int)db.ALLAT.Count(x=>x.)(x => x.DATUM == akt && x.TIPUS == "eledel").Sum(x => x.MENNYISEG));
        //    //}
        //    return list;
        //}

    }//end Statisztika

}//end namespace AdatKezelő
