using AdatKezelő.csillamRef;
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
    public class Ügyfél_Kezelő : IÜgyfél_kezelő
    {
        Admin_kezelő adminKezelő;
        csillamponiDBEntities db;
       // List<ALLAT> allatok = new List<ALLAT>();
        public Admin_kezelő AdminKezelő
        {
            get { return adminKezelő; }
            set { adminKezelő = value; }
        }

        public csillamponiDBEntities Db
        {
            get { return db; }
            set { db = value; }
        }
       
        public Ügyfél_Kezelő()
        {
            adminKezelő = new Admin_kezelő();
            db = new csillamponiDBEntities();
        }

        public void Adományoz(Adomány_típus típus, int mennyiség, string ki)
        {
            var q = from x in adminKezelő.Db.UGYFEL
                    where x.USERNAME == ki
                    select x.UGYFELID;
            if (q.Count() != 0)
            {
                Guid userGuid = q.First();
                adminKezelő.Adományoz(userGuid, "", mennyiség, típus);
            }
        }

        public string GenerateAdvString()
        {// Luca
            var allatok = from allat in db.ALLAT
                          where allat.ELOJEGYZETT == false
                          select new { Név = allat.NEV, Fajta = allat.FAJTA};

            string advString = "";

            foreach (var item in allatok)
            {
                advString += string.Format("***Egy {1} keresi gazdáját, neve : {1}!",item.Név, item.Fajta);
            }
            return advString;
        }

        public bool Előjegyeztet(ALLAT állat)
        {
            ALLAT orig = db.ALLAT.Find(állat.ALLATID);
            orig.ELOJEGYZETT = true;
            db.SaveChanges();
            return true;
        }

        public void Örökbe_ad(ALLAT allat, UGYFEL kicsoda)
        {
            Service1Client client = new Service1Client();
            if (kicsoda.ISADMIN == false)
            {
                client.sendEmail("csillamponiproject@gmail.com", "Örökbeadás", kicsoda.VEZETEKNEV + " " + kicsoda.KERESZTNEV + " új állatot vett fel az adatbázisba. Kérlek szólj az illetékeseknek, hogy új állat érkezik!");
            }
        }


        public int Van_e_üres_kennel(string allatfaj)
        {
            var uresHelyek = from karam in db.KENNEL
                             where karam.TIPUS == allatfaj
                             select karam.SZABAD;

            if (uresHelyek != null)
            {
                return (int)uresHelyek.FirstOrDefault();
            }
            else
            {
                return 0;
            }
        }

        List<ALLAT>  IÜgyfél_kezelő.Fajtára_keres(string fajta)
        {
            List<ALLAT> allatok = new List<ALLAT>();
            var q = from x in adminKezelő.Db.ALLAT
                    where x.FAJTA == fajta.ToUpper()
                    select x;
            foreach (var item in q)
            {
                allatok.Add(item);
            }
            return allatok;
        }

        List<ALLAT> IÜgyfél_kezelő.Méretre_keres(int kilogramm)
        {
            List<ALLAT> allatok = new List<ALLAT>();
            var q = from x in adminKezelő.Db.ALLAT
                    where x.TOMEG==kilogramm
                    select x;
            foreach (var item in q)
            {
                allatok.Add(item);
            }
            return allatok;
        }
        List<ALLAT> IÜgyfél_kezelő.Színre_keres(string szín)
        {
            List<ALLAT> allatok = new List<ALLAT>();
            var q = from x in adminKezelő.Db.ALLAT
                    where x.SZIN == szín.ToUpper()
                    select x;
            foreach (var item in q)
            {
                allatok.Add(item);
            }
            return allatok;
        }
        List<ALLAT> IÜgyfél_kezelő.Összetett_keresés(bool oltva, bool nem, string szín, string fajta, string név, bool ivartalanított, bool beteg)
        {// Luca
         //(bocsi de módosítottam a keresést mert nem nagyon akart visszaadni állatot ha nem tudtam az összes property-jét)
            List<ALLAT> allatok = new List<ALLAT>();


            var allatQuery = db.ALLAT.Where(x => (!string.IsNullOrEmpty(név) ? x.NEV == név : true) && (oltva != null ? x.OLTVA == oltva : true)
                 && (nem != null ? x.NOSTENY == nem : true) && (!string.IsNullOrEmpty(fajta) ? x.FAJTA == fajta : true)
                && (ivartalanított != null ? x.IVARTALANITOTT == ivartalanított : true) && (!string.IsNullOrEmpty(szín) ? x.SZIN == szín : true)
                && ((beteg != null && beteg == false) ? string.IsNullOrEmpty(x.BETEGSEGEK) : !string.IsNullOrEmpty(x.BETEGSEGEK)));
            //a kereséssel van még némi probléma
            
            foreach (var item in allatQuery)
            {
                if (item.ELOJEGYZETT == false) //a már előjegyzett állatokat ne listázza örökbefogadásra
                {
                    Console.WriteLine(item.ToString());
                    allatok.Add(item);
                }
            }
            return allatok;
            //if (!beteg)
            //{
            //    var q = from x in adminKezelő.Db.ALLAT
            //            where x.NEV == név.ToUpper() &&
            //            x.SZIN == szín.ToUpper() &&
            //            x.FAJTA == fajta.ToUpper() &&
            //            x.NOSTENY == nem &&
            //            x.OLTVA == oltva &&
            //            x.IVARTALANITOTT == ivartalanított &&
            //            string.IsNullOrEmpty(x.BETEGSEGEK)
            //            select x;
            //    foreach (var item in q)
            //    {
            //        allatok.Add(item);
            //    }
            //}
            //else
            //{
            //    var q = from x in adminKezelő.Db.ALLAT
            //            where x.NEV == név.ToUpper() &&
            //            x.SZIN == szín.ToUpper() &&
            //            x.FAJTA == fajta.ToUpper() &&
            //            x.NOSTENY == nem &&
            //            x.OLTVA == oltva &&
            //            x.IVARTALANITOTT == ivartalanított
            //            select x;
            //    foreach (var item in q)
            //    {
            //        allatok.Add(item);
            //    }
            //}
            //return allatok;
        }

       

           
        

        public List<String> KennelListafeltolt()
        {
            List<String> tipusok = new List<String>();
            var q = from x in adminKezelő.Db.KENNEL
                    select x.TIPUS;
            foreach (var item in q)
            {
                tipusok.Add(item);
            }
            return tipusok;
        }
    }//end Ügyfél_Kezelő

}//end namespace AdatKezelő