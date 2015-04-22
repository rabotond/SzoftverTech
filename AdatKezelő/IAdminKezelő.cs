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
        
        void Eledelt_hozzáad(ELEDEL e, int mennyit);
        void Állatot_hozzáad(ALLAT állat);

        void Állatot_módosít(ALLAT állat);

        void Állatot_töröl(ALLAT állat);

        void Ügyfelet_hozzáad(UGYFEL ügyfél);

        void Ügyfelet_módosít(UGYFEL ügyfél);

        void Ügyfelet_töröl(UGYFEL ügyfél);
        
        void Adományoz(Guid ki, string mikor, int mennyit, Adomány_típus tipus);

        void Előjegyzést_végez(ALLAT állat);

        Statisztika Statisztikát_készít(Statisztika_típus fajta, DateTime idoszak_kezdet,DateTime idoszak_vege);
    
    }//end IAdmin_kezelő

}//end namespace AdatKezelő