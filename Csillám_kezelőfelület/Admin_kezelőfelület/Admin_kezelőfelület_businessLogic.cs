﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AdatKezelő;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;
using System.Collections;

namespace Csillám_kezelőfelület.Admin_kezelőfelület
{
    public class Admin_kezelőfelület_businessLogic : IAdmin_kezelő //Molnár Fanni
    {
        Admin_kezelő kezelo;

        public Admin_kezelőfelület_businessLogic()
        {
            kezelo = new Admin_kezelő();
        }

        public void ExportToExcel(XDocument doc, string fileName)
        {
            Statisztika stat = new Statisztika();
            stat.ExportToExcel(doc,fileName);
        }

        public bool KennelTablaHelyKarbanTartas(string faj)
        {
            return  kezelo.KennelTablaHelyKarbanTartas(faj);
        }

        public void KennelTablaSync()
        {
            kezelo.KennelTablaSync();
        }

        public Statisztika Statisztikát_készít(Statisztika_típus fajta, DateTime idoszak_kezdet, DateTime idoszak_vege)
        {
            return kezelo.Statisztikát_készít(fajta, idoszak_kezdet, idoszak_vege);
        }

        public List<eledel_kennel_VM> FrissitEledel_kennel()
        {
            return kezelo.getAllEledel_kennel();
        }
        
        public List<AllatVM> FrissitAllat()
        { 
            return kezelo.getAllAllat();
        }
      
        public List<UgyfelVM> FrissitÜgyfel()
        {
            return kezelo.getAllÜgyfél();
        }

        public void Adományoz(Guid ki, string mikor, int mennyit, Adomány_típus tipus)
        {
            kezelo.Adományoz(ki, mikor, mennyit, tipus);
        }

        public void Előjegyzést_végez(ALLAT állat)
        {
            kezelo.Előjegyzést_végez(állat);       
        }

        public void Állatot_hozzáad(ALLAT állat)
        {
             kezelo.Állatot_hozzáad(állat);
        }

        public void Állatot_módosít(AllatVM állat)
        {
             kezelo.Állatot_módosít(állat);
        }

        public void Állatot_töröl(AllatVM állat)
        {
             kezelo.Állatot_töröl(állat);
        }

        public void Ügyfelet_hozzáad(UgyfelVM ügyfél)
        {
             kezelo.Ügyfelet_hozzáad(ügyfél);
        }

        public void Ügyfelet_módosít(UgyfelVM ügyfél)
        {
             kezelo.Ügyfelet_módosít(ügyfél);
        }

        public void Ügyfelet_töröl(UgyfelVM ügyfél)
        {
             kezelo.Ügyfelet_töröl(ügyfél);
        }

        public void Eledelt_kennelt_hozzáad(ELEDEL e,KENNEL k, int mennyit)
        {
            kezelo.Eledelt_kennelt_hozzáad(e,k, mennyit);
        }
    }
}
