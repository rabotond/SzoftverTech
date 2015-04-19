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
        
        bool Állatot_hozzáad(ALLAT állat);

        bool Állatot_módosít(ALLAT állat);

        bool Állatot_töröl(ALLAT állat);

        bool Ügyfelet_hozzáad(UGYFEL ügyfél);

        bool Ügyfelet_módosít(UGYFEL ügyfél);

        bool Ügyfelet_töröl(UGYFEL ügyfél);
        
        void Adományoz(Guid ki, string mikor, int mennyit, Adomány_típus tipus);

        bool Előjegyzést_végez(ALLAT állat);

        Statisztika Statisztikát_készít(Statisztika_típus fajta, DateTime idoszak_kezdet,DateTime idoszak_vege);
    
    }//end IAdmin_kezelő

}//end namespace AdatKezelő