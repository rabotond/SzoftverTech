using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AdatKezelő;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;

namespace Csillám_kezelőfelület.Admin_kezelőfelület
{
    class Admin_kezelőfelület_businessLogic : IAdmin_kezelő
    {
        Admin_kezelő kezelo;

        public Admin_kezelőfelület_businessLogic()
        {
            kezelo = new Admin_kezelő();
        }

        public Statisztika Statisztikát_készít(string fajta, DateTime idoszak_kezdet, DateTime idoszak_vege)
        {
            Statisztika stat = kezelo.Statisztikát_készít(fajta, idoszak_kezdet, idoszak_vege);

            XDocument doc = new XDocument();
            foreach (DateTime datum in stat.Napok)
            {
                XElement date = new XElement("Dátum", datum);

            }

            return stat;
        }

        public List<object> FrissitAllat()
        {
           // return kezelo.getAllAllat();
            var q =
           from allat in kezelo.Db.ALLAT
           select new { név = allat.NEV, fajta=allat.FAJTA,oltva = allat.OLTVA, elojegyzett = allat.ELOJEGYZETT,ivartalanított=allat.IVARTALANITOTT,chipezett=allat.CHIPES,született=allat.SZULETESI_IDO,színe=allat.SZIN };
            return q.ToList<object>();
        }

        public List<object> FrissitÜgyfel()
        {
            var q =
            from ugyfel in kezelo.Db.UGYFEL
            select new { név = ugyfel.VEZETEKNEV+" "+ugyfel.KERESZTNEV, város = ugyfel.VAROS, email = ugyfel.EMAIL };
            return q.ToList<object>();

           
        }

        public void Adományoz(Guid ki, string mikor, int mennyit, Adomány_típus tipus)
        {
            kezelo.Adományoz(ki, mikor, mennyit, tipus);
        }

        public bool Előjegyzést_végez(ALLAT állat)
        {
           return kezelo.Előjegyzést_végez(állat);       
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
