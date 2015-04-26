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
    public interface IAdmin_kezelő
    {
        void KennelTablaSync();

        bool KennelTablaHelyKarbanTartas(string tipus);

        void Eledelt_kennelt_hozzáad(ELEDEL e, KENNEL k, int mennyit);
        
        void Állatot_hozzáad(ALLAT állat);

        void Állatot_módosít(AllatVM állat);

        void Állatot_töröl(AllatVM állat);

        void Ügyfelet_hozzáad(UgyfelVM ügyfél);

        void Ügyfelet_módosít(UgyfelVM ügyfél);

        void Ügyfelet_töröl(UgyfelVM ügyfél);
        
        void Adományoz(Guid ki, string mikor, int mennyit, Adomány_típus tipus);

        void Előjegyzést_végez(ALLAT állat);

        Statisztika Statisztikát_készít(Statisztika_típus fajta, DateTime idoszak_kezdet,DateTime idoszak_vege);
    
    }//end IAdmin_kezelő

}//end namespace AdatKezelő