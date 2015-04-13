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
        void Adományoz(Guid ki, string mikor, int mennyit, Adomány_típus tipus);

        void Database_communication();

        bool Előjegyzést_végez(ALLAT állat);

        Statisztika Statisztikát_készít(string fajta);
    }//end IAdmin_kezelő

}//end namespace AdatKezelő