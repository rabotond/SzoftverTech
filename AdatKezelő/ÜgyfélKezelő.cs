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

        public Ügyfél_Kezelő()
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