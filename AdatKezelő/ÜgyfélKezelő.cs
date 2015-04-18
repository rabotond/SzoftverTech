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
                    where x.EMAIL == ki
                    select x.UGYFELID;
            if (q.Count() != 0)
            {
                Guid userGuid = q.First();
                adminKezelő.Adományoz(userGuid, "wtf", mennyiség, típus);
            }

        }

        public bool Előjegyeztet(ALLAT állat)
        {

            return false;
        }

        public void Örökbe_ad(ALLAT allat, UGYFEL kicsoda)
        {

        }

        public void Örökbe_fogad(ALLAT allat, UGYFEL kicsoda)
        {

        }


        public bool Van_e_üres_kennel(ALLAT melyik_állat_számára)
        {

            return false;
        }

        ALLAT IÜgyfél_kezelő.Fajtára_keres(string fajta)
        {
            throw new NotImplementedException();
        }

        ALLAT IÜgyfél_kezelő.Méretre_keres(int kilogramm)
        {
            throw new NotImplementedException();
        }

        ALLAT IÜgyfél_kezelő.Összetett_keresés(bool sterilizált, bool nem, int kor, string szín, string fajta)
        {
            throw new NotImplementedException();
        }

        ALLAT IÜgyfél_kezelő.Színre_keres(string szín)
        {
            throw new NotImplementedException();
        }
    }//end Ügyfél_Kezelő

}//end namespace AdatKezelő