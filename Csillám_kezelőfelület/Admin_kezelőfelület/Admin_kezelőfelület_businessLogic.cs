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
        List<ALLAT> allatlista;
        List<UGYFEL> ugyfellista;
        public Admin_kezelőfelület_businessLogic()
        {
            allatlista = kezelo.getAllAllat();
            ugyfellista = kezelo.getAllÜgyfél();
        }

        public void Adományoz(Guid ki, string mikor, int mennyit, Adomány_típus tipus)
        {
            kezelo.Adományoz(ki, mikor, mennyit, tipus);
        }

        public bool Előjegyzést_végez(ALLAT állat)
        {
            kezelo.Előjegyzést_végez(állat);
            return true;
        }

        public Statisztika Statisztikát_készít(string fajta, DateTime idoszak_kezdet, DateTime idoszak_vege)
        {
            return kezelo.Statisztikát_készít(fajta, idoszak_kezdet, idoszak_vege);
        }

        public bool Állatot_hozzáad(ALLAT állat)
        {
            throw new NotImplementedException();
        }

        public bool Állatot_módosít(ALLAT állat)
        {
            throw new NotImplementedException();
        }

        public bool Állatot_töröl(ALLAT állat)
        {
            throw new NotImplementedException();
        }

        public bool Ügyfelet_hozzáad(UGYFEL ügyfél)
        {
            throw new NotImplementedException();
        }

        public bool Ügyfelet_módosít(UGYFEL ügyfél)
        {
            throw new NotImplementedException();
        }

        public bool Ügyfelet_töröl(UGYFEL ügyfél)
        {
            throw new NotImplementedException();
        }
    }
}
