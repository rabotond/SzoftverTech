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
        csillamponimenhelyDBEntities db;
        
        public Admin_kezelő AdminKezelő
        {
            get { return adminKezelő; }
            set { adminKezelő = value; }
        }     

        public csillamponimenhelyDBEntities Db
        {
            get { return db; }
            set { db = value; }
        }
       
        public Ügyfél_Kezelő()
        {
            adminKezelő = new Admin_kezelő();
            db = new csillamponimenhelyDBEntities();
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

        public bool Előjegyeztet(ALLAT állat)
        {
            var q = from x in adminKezelő.Db.ALLAT
                    where x == állat
                    select x.ELOJEGYZETT==true;

            return true;
        }

        public void Örökbe_ad(ALLAT allat, UGYFEL kicsoda)
        {

        }

        public void Örökbe_fogad(ALLAT allat, UGYFEL kicsoda)
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

        List< ALLAT> IÜgyfél_kezelő.Összetett_keresés(bool sterilizált, bool nem, string szín, string fajta,string név)
        {
            List<ALLAT> allatok = new List<ALLAT>();
            var q = from x in adminKezelő.Db.ALLAT
                    where x.NEV == név.ToUpper() && x.SZIN == szín.ToUpper() && x.FAJTA == fajta.ToUpper() && x.NOSTENY == nem && x.OLTVA == sterilizált
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
    }//end Ügyfél_Kezelő

}//end namespace AdatKezelő