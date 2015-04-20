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
        void Adományoz(Adomány_típus típus, int mennyiség, string ki);

        /// 
        /// <param name="állat"></param>
        bool Előjegyeztet(ALLAT állat);

        /// 
        /// <param name="fajta"></param>
        List<ALLAT> Fajtára_keres(string fajta);

        /// 
        /// <param name="kilogramm"></param>
        List<ALLAT> Méretre_keres(int kilogramm);

        /// 
        /// <param name="mikor"></param>
        /// <param name="mit"></param>
        /// <param name="ki"></param>
        void Örökbe_ad(ALLAT allat,UGYFEL kicsoda);

       // változtattam
        void Örökbe_fogad(ALLAT allat, UGYFEL kicsoda);

      // változtatva
        List< ALLAT> Összetett_keresés(bool sterilizált, bool nem, string szín, string fajta,string név);

        /// 
        /// <param name="szín"></param>
       List< ALLAT> Színre_keres(string szín);

        /// 
        /// <param name="melyik_állat_számára"></param>
        int Van_e_üres_kennel(string allatfaj);
    }//end IÜgyfél_kezelő

}//end namespace AdatKezelő