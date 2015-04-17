using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AdatKezelő;

namespace Csillám_kezelőfelület.Admin_kezelőfelület
{
    class Admin_kezelőfelület_businessLogic : IAdmin_kezelő
    {
        Admin_kezelő kezelo;

        public Admin_kezelőfelület_businessLogic()
        {
            kezelo = new Admin_kezelő();

        }
        public List<ALLAT> FrissitAllat()
        {
            return kezelo.getAllAllat();
        }
        public List<UGYFEL> FrissitÜgyfel()
        {
            return kezelo.getAllÜgyfél();
        }

        public void Adományoz(Guid ki, string mikor, int mennyit, Adomány_típus tipus)
        {
            kezelo.Adományoz(ki, mikor, mennyit, tipus);
        }

        public bool Előjegyzést_végez(ALLAT állat)
        {
           return kezelo.Előjegyzést_végez(állat);
            
        }

        public Statisztika Statisztikát_készít(string fajta, DateTime idoszak_kezdet, DateTime idoszak_vege)
        {
            return kezelo.Statisztikát_készít(fajta, idoszak_kezdet, idoszak_vege);
        }

        public bool Állatot_hozzáad(ALLAT állat)
        {
            return kezelo.Állatot_hozzáad(állat);
        }

        public bool Állatot_módosít(ALLAT állat)
        {
            return kezelo.Állatot_módosít(állat);
        }

        public bool Állatot_töröl(ALLAT állat)
        {
            return kezelo.Állatot_töröl(állat);
        }

        public bool Ügyfelet_hozzáad(UGYFEL ügyfél)
        {
            return kezelo.Ügyfelet_hozzáad(ügyfél);
        }

        public bool Ügyfelet_módosít(UGYFEL ügyfél)
        {
            return kezelo.Ügyfelet_módosít(ügyfél);
        }

        public bool Ügyfelet_töröl(UGYFEL ügyfél)
        {
            return kezelo.Ügyfelet_töröl(ügyfél);

        }
    }
}
