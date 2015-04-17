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
    public interface IÜgyfél_kezelő
    {

        /// 
        /// <param name="adomány"></param>
        void Adományoz(ADOMANY adomány);

        /// 
        /// <param name="állat"></param>
        bool Előjegyeztet(ALLAT állat);

        /// 
        /// <param name="fajta"></param>
        ALLAT Fajtára_keres(string fajta);

        /// 
        /// <param name="kilogramm"></param>
        ALLAT Méretre_keres(int kilogramm);

        /// 
        /// <param name="mikor"></param>
        /// <param name="mit"></param>
        /// <param name="ki"></param>
        void Örökbe_ad(ALLAT allat,UGYFEL kicsoda);

       // változtattam
        void Örökbe_fogad(ALLAT allat, UGYFEL kicsoda);

      // változtatva
        ALLAT Összetett_keresés(bool sterilizált, bool nem, int kor, string szín, string fajta);

        /// 
        /// <param name="szín"></param>
        ALLAT Színre_keres(string szín);

        /// 
        /// <param name="melyik_állat_számára"></param>
        bool Van_e_üres_kennel(ALLAT melyik_állat_számára);
    }//end IÜgyfél_kezelő

}//end namespace AdatKezelő