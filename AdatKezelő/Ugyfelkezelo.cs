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
    public class Ugyfelkezelo : IUgyfelkezelo
    {

        public Ugyfelkezelo()
        {

        }

        public virtual void Dispose()
        {

        }

        /// 
        /// <param name="adomány"></param>
        public void Adományoz(ADOMANY adomány)
        {

        }

        public bool Előjegyeztet(ALLAT állat)
        {

            return false;
        }

        public void Örökbe_ad(string mikor, string mit, int ki)
        {

        }

        public void Örökbe_fogad(string mikor, int melyik_állatot, int Ki)
        {

        }


        public bool Van_e_üres_kennel(ALLAT melyik_állat_számára)
        {

            return false;
        }

        ALLAT IUgyfelkezelo.Fajtára_keres(string fajta)
        {
            throw new NotImplementedException();
        }

        ALLAT IUgyfelkezelo.Méretre_keres(int kilogramm)
        {
            throw new NotImplementedException();
        }

        ALLAT IUgyfelkezelo.Összetett_keresés(bool sterilizált, bool nem, int kor, string szín, string fajta)
        {
            throw new NotImplementedException();
        }

        ALLAT IUgyfelkezelo.Színre_keres(string szín)
        {
            throw new NotImplementedException();
        }

        IEnumerable<string> IUgyfelkezelo.GetAllatfajok()
        {
            csillamponimenhelyDBEntities db = new csillamponimenhelyDBEntities();
            var fajok = from allat in db.ALLAT
                        select allat.FAJTA;

            return fajok;
        }
    }//end Ügyfél_Kezelő

}//end namespace AdatKezelő