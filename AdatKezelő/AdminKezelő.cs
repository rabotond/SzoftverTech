

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace AdatKezelő
{
    public class Admin_kezelő : IAdmin_kezelő
    {
        csillamponimenhelyDBEntities db;
        static readonly object loadlock = new object();

        public csillamponimenhelyDBEntities Db
        {
            get { return db; }
            set { db = value; }
        }

        public Admin_kezelő()
        {
            db = new csillamponimenhelyDBEntities();
        }

        public List<eledel_kennel_VM> getAllEledel_kennel()
        {
            lock (loadlock)
            {     
                db = new csillamponimenhelyDBEntities();
                var lista = (from a in db.ELEDEL
                             from b in db.KENNEL
                             where a.FAJTA == b.TIPUS
                             select new eledel_kennel_VM() { TIPUS = a.FAJTA, MAXkennel = b.MAXDARAB, SZABAD_kennelek = b.SZABAD, FOGLALT_kennelek = b.FOGLALT, RAKTARON_kg = (int)a.RAKTARON });
                return lista.ToList();
            }
        }
       
        public List<AllatVM> getAllAllat() // datagrid megjelenítéshez
        {
            lock (loadlock)
            {
                db = new csillamponimenhelyDBEntities();
                return db.ALLAT.Where(x => x.NEV != null).Select(x => new AllatVM()
                {
                    ALLATID = x.ALLATID,
                    BEADVA = x.BEADVA,
                    OROKBEFOGADVA = x.OROKBEFOGADVA,
                    CHIPES = x.CHIPES,
                    NEV = x.NEV,
                    ELOZO_TULAJ = x.ELOZO_TULAJ,
                    IVARTALANITOTT = x.IVARTALANITOTT,
                    FAJTA = x.FAJTA,
                    NOSTENY = x.NOSTENY,
                    BETEGSEGEK = x.BETEGSEGEK,
                    ELOJEGYZETT = x.ELOJEGYZETT,
                    OLTVA = x.OLTVA,
                    SZIN = x.SZIN,
                    SZULETESI_IDO = x.SZULETESI_IDO,
                    TOMEG = x.TOMEG,
                    MERET = x.MERET,
                    kep=x.KEP
                }).ToList();
            }
        }

        public List<UgyfelVM> getAllÜgyfél() // datagrid megjeleítéshez
        {
            lock (loadlock)
            {
                return db.UGYFEL.Where(x => x.VEZETEKNEV != null).Select(x => new UgyfelVM()
                {
                    UGYFELID = x.UGYFELID,
                    VEZETEKNEV = x.VEZETEKNEV,
                    KERESZTNEV = x.KERESZTNEV,
                    VAROS = x.VAROS,
                    UTCA = x.UTCA,
                    HAZSZAM = x.HAZSZAM,
                    EMAIL = x.EMAIL,
                    TELEFON = x.TELEFON
                }).ToList();
            }
        }

        public void Adományoz(Guid ki, string mikor, int mennyit, Adomány_típus mit)
        {
            ADOMANY a = new ADOMANY();
            a.ADOMANYID = Guid.NewGuid();
            a.ADOMANYOZO = ki;
            a.DATUM = DateTime.Now;
            a.MENNYISEG = mennyit;
            a.TIPUS = mit.ToString();
            db.ADOMANY.Add(a);
            db.SaveChanges();
        }

        public void Eledelt_kennelt_hozzáad(ELEDEL e,KENNEL k, int mennyit)
        {
            if(e!=null)
            {
                var q = from eledel in db.ELEDEL
                        where eledel.FAJTA == e.FAJTA
                        select eledel;
                if ((q.FirstOrDefault().RAKTARON+=mennyit)<=0)
                {
                    q.FirstOrDefault().RAKTARON = 0;
                }
                else
                {
                    q.FirstOrDefault().RAKTARON += mennyit;
                }
            }
            else
            {
                var q = from kennel in db.KENNEL
                        where kennel.TIPUS == k.TIPUS
                        select kennel;
                if ((q.FirstOrDefault().MAXDARAB += mennyit) <= 0)
                {
                    q.FirstOrDefault().MAXDARAB = 0;
                }
                else
                {
                    q.FirstOrDefault().MAXDARAB += mennyit;
                }
            }
            db.SaveChanges();
        }

        public void Előjegyzést_végez(ALLAT allat)
        {
            ALLAT a=  db.ALLAT.Find(allat.ALLATID);
            a.ELOJEGYZETT = true;
            db.SaveChanges();
        }

        public void Állatot_hozzáad(ALLAT állat)
        {
            try
            {
                állat.ALLATID = Guid.NewGuid();
                db.ALLAT.Add(állat);
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }

        }

        public void Állatot_módosít(AllatVM állat)//lecserélem az egészet..id marad
        {
         
            db.ALLAT.Remove(db.ALLAT.Find(állat.ALLATID));
            ALLAT a = (ALLAT)convert_vm_entity(állat, null);
            db.ALLAT.Add(a);
            db.SaveChanges();
        }

        public void Állatot_töröl(AllatVM állat)
        {
            ALLAT a = db.ALLAT.Find(állat.ALLATID);
            db.ALLAT.Remove(a);
            db.SaveChanges();
        }

        public void Ügyfelet_hozzáad(UgyfelVM ügyfél)
        {
            UGYFEL client = new UGYFEL();
            client.UGYFELID = Guid.NewGuid();
            client = (UGYFEL) convert_vm_entity(null,ügyfél);
            db.UGYFEL.Add(client);
            db.SaveChanges();
        }

        public void Ügyfelet_töröl(UgyfelVM ügyfél)
        {
            try
            {
                UGYFEL a = db.UGYFEL.Find(ügyfél.UGYFELID);
                db.UGYFEL.Remove(a);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                //magic
            }
            
        }

        public void Ügyfelet_módosít(UgyfelVM ügyfél)
        {
            db.UGYFEL.Remove(db.UGYFEL.Find(ügyfél.UGYFELID));
            UGYFEL uj= (UGYFEL)convert_vm_entity(null, ügyfél);
            db.UGYFEL.Add(uj);
            db.SaveChanges();
        }

        public Statisztika Statisztikát_készít(Statisztika_típus fajta, DateTime idoszak_kezdet, DateTime idoszak_vege)
        {
            Statisztika stat = new Statisztika(fajta, idoszak_kezdet, idoszak_vege);
            return stat;
        }
        
        public void KennelTablaSync() // készítette Kovács Luca
        {
            var allatokByFajta = from allat in db.ALLAT
                                 group allat by allat.FAJTA into g
                                 select g;

            foreach (var item in allatokByFajta)
            {
                var kennels = from actKennel in db.KENNEL
                              where actKennel.TIPUS == item.Key
                              select actKennel;

                if (kennels.Count() != 0)
                {
                    var kennel = kennels.FirstOrDefault();

                    kennel.FOGLALT = item.Count();

                    kennel.SZABAD = kennel.MAXDARAB - kennel.FOGLALT;
                   // db.SaveChanges();
                }
                else
                {
                    KENNEL newKennel = new KENNEL() { FOGLALT = item.Count(), MAXDARAB = 100, SZABAD = 100 - item.Count(), TIPUS = item.Key };
                }

            }
            db.SaveChanges();
        }

        public bool KennelTablaHelyKarbanTartas(string tipus)
        {// készítette Kovács Luca
            var q = from kennel in db.KENNEL
                    where kennel.TIPUS == tipus
                    select kennel;
            if (q.Count() != 0)
            {
                if (q.FirstOrDefault().SZABAD == 0)
                    return false;
                q.FirstOrDefault().SZABAD--;
                q.FirstOrDefault().FOGLALT++;
                db.SaveChanges();
                return true;
            }
            else
            {
                this.KennelTablaSync();
            }
            return true;
        }

        private object convert_vm_entity(AllatVM a,UgyfelVM ügyfél)
        {
            if(a!=null)
            {
                ALLAT allat = new ALLAT();
                allat.ALLATID = a.ALLATID;
                allat.NEV = a.NEV;
                allat.SZIN = a.SZIN;
                allat.SZULETESI_IDO = a.SZULETESI_IDO;
                allat.ELOJEGYZETT = a.ELOJEGYZETT;
                allat.FAJTA = a.FAJTA;
                allat.BEADVA = a.BEADVA;
                allat.CHIPES = a.CHIPES;
                allat.IVARTALANITOTT = a.IVARTALANITOTT;
                allat.KEP = a.kep;
                allat.OLTVA = a.OLTVA;
                allat.NOSTENY = a.NOSTENY;
                allat.BETEGSEGEK = a.BETEGSEGEK;
                allat.MERET = a.MERET;
                allat.TOMEG = a.TOMEG;
                allat.OROKBEFOGADVA = a.OROKBEFOGADVA;
                return allat;
            }
            else
            {
                UGYFEL client = new UGYFEL();
                client.UGYFELID = ügyfél.UGYFELID;
                client.EMAIL = ügyfél.EMAIL;
                client.HAZSZAM = ügyfél.HAZSZAM;
                client.IRSZ = ügyfél.IRSZ;
                client.ISADMIN = ügyfél.isadmin;
                client.KERESZTNEV = ügyfél.KERESZTNEV;
                client.REGDATUM = DateTime.Now;
                client.TELEFON = ügyfél.TELEFON;
                client.VAROS = ügyfél.VAROS;
                client.VEZETEKNEV = ügyfél.VEZETEKNEV;
                client.USERNAME = ügyfél.USERNAME;
                client.PASSWORD = ügyfél.PASSWORD;
                client.UTCA = ügyfél.UTCA;
                return client;
            }
        }
    }//end Admin_kezelő

}//end namespace AdatKezelő
