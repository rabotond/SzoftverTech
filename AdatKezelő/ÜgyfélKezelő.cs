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
            var q = from x in adminKezelő.Db.ALLAT
                    where x == állat
                    select x.ELOJEGYZETT==true;
            return true;
        }

        public void Örökbe_ad(ALLAT allat, UGYFEL kicsoda)//srácok ez mért nincs implementálva,---fanni
        {

        }

        public void Örökbe_fogad(ALLAT allat, UGYFEL kicsoda)//srácok ez mért nincs implementálva,---fanni
        {

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

        List<ALLAT> IÜgyfél_kezelő.Összetett_keresés(bool oltva, bool nem, string szín, string fajta, string név, bool ivartalanított, bool beteg)
        {
            List<ALLAT> allatok = new List<ALLAT>();
            
            if (!beteg)
            {
                var q = from x in adminKezelő.Db.ALLAT
                        where x.NEV == név.ToUpper() &&
                        x.SZIN == szín.ToUpper() &&
                        x.FAJTA == fajta.ToUpper() &&
                        x.NOSTENY == nem &&
                        x.OLTVA == oltva &&
                        x.IVARTALANITOTT == ivartalanított &&
                        string.IsNullOrEmpty(x.BETEGSEGEK)
                        select x;
                foreach (var item in q)
                {
                    allatok.Add(item);
                }
            }
            else
            {
                var q = from x in adminKezelő.Db.ALLAT
                        where x.NEV == név.ToUpper() &&
                        x.SZIN == szín.ToUpper() &&
                        x.FAJTA == fajta.ToUpper() &&
                        x.NOSTENY == nem &&
                        x.OLTVA == oltva &&
                        x.IVARTALANITOTT == ivartalanított
                        select x;
                foreach (var item in q)
                {
                    allatok.Add(item);
                }
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