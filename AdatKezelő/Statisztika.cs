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
        Dictionary<string, List<object>> stat_listak;
        List<Task> tasklist;
        XDocument xdoc;

        public XDocument xDoc
        {
            get { return xdoc; }
            set { xdoc = value; }
        }

        public void MakeStatistic()
        {
            if(tipus==Statisztika_típus.állatállomány)
            {
               stat_listak.Add( "elvittAllatok", elvittAllatDb());
               stat_listak.Add("hozottAllatok", hozottAllatDb());
               stat_listak.Add("szabadKennelek", szabadKennelDb());
            }
            else if(tipus==Statisztika_típus.adomány)
            {
               stat_listak.Add("penzadomany",befolyPenz());
               stat_listak.Add( "eledeladomany",kapottEledel());
            }
            else
            {
                stat_listak.Add("elvittAllatok", elvittAllatDb());
                stat_listak.Add("hozottAllatok", hozottAllatDb());
                stat_listak.Add("szabadKennelek", szabadKennelDb());
                stat_listak.Add("penzadomany", befolyPenz());
                stat_listak.Add("eledeladomany", kapottEledel());
            }
        }

        public Dictionary<string, List<object>> Stat_listak
        {
            get { return stat_listak; }
            set { stat_listak = value; }
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

        public Statisztika_típus Tipus
        {
            get { return tipus; }
            set { tipus = value; }
        }

        public Statisztika(Statisztika_típus ujtipus, DateTime ujmettől, DateTime ujmeddig)
        {
            mettől = ujmettől; meddig = ujmeddig; tipus = ujtipus;
            napok = new List<DateTime>();
            db = new csillamponimenhelyDBEntities();
            stat_listak = new Dictionary<string, List<object>>();
            tasklist = new List<Task>();
            for (int i = 0; i < (meddig - mettől).TotalDays;i++ )
            {
                napok.Add(mettől.AddDays(i));
            }
        }

        public List<object> elvittAllatDb()
        {
            List<object> list = new List<object>();
            foreach(DateTime akt in napok)
            {
                list.Add(db.ALLAT.Count(x => x.OROKBEFOGADVA==akt));
            }
            return list;
        }
        public List<object> hozottAllatDb()
        {
            List<object> list = new List<object>();
            foreach (DateTime akt in napok)
            {
                list.Add(db.ALLAT.Count(x => x.BEADVA == akt));
            }
            return list;
        }

        public List<object> befolyPenz()
        {
            List<object> list = new List<object>();
            foreach (DateTime akt in napok)
            {  
                list.Add( (int)db.ADOMANY.Where(x => x.DATUM == akt && x.TIPUS == "pénz").Sum(x => x.MENNYISEG));
            }
            return list;
        }

        public List<object> kapottEledel()
        {
            List<object> list = new List<object>();
            foreach (DateTime akt in napok)
            {
                list.Add((int)db.ADOMANY.Where(x => x.DATUM == akt && x.TIPUS == "eledel").Sum(x => x.MENNYISEG));
            }
            return list;
        }
        public List<object> szabadKennelDb()
        {
            List<object> list = new List<object>();
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
