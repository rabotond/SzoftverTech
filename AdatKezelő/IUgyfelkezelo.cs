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
    public interface IUgyfelkezelo
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
        void Örökbe_ad(string mikor, string mit, int ki);

        /// 
        /// <param name="mikor"></param>
        /// <param name="melyik_állatot"></param>
        /// <param name="Ki"></param>
        void Örökbe_fogad(string mikor, int melyik_állatot, int Ki);

        /// 
        /// <param name="sterilizált"></param>
        /// <param name="nem"></param>
        /// <param name="kor"></param>
        /// <param name="szín"></param>
        /// <param name="fajta"></param>
        ALLAT Összetett_keresés(bool sterilizált, bool nem, int kor, string szín, string fajta);

        /// 
        /// <param name="szín"></param>
        ALLAT Színre_keres(string szín);

        /// 
        /// <param name="melyik_állat_számára"></param>
        bool Van_e_üres_kennel(ALLAT melyik_állat_számára);

        IEnumerable<string> GetAllatfajok();
    }//end IÜgyfél_kezelő

}//end namespace AdatKezelő