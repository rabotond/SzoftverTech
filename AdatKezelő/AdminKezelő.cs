using System;
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

            public List<AllatVM> getAllAllat()
            {
                lock (loadlock)
                {
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
                        MERET = x.MERET
                    }).ToList();
                }
            }

            public List<UgyfelVM> getAllÜgyfél()
           {
               lock (loadlock)
               {
                   return db.UGYFEL.Where(x => x.VEZETEKNEV != null).Select(x => new UgyfelVM()
                   {
                       UGYFELID=x.UGYFELID,
                       VEZETEKNEV=x.VEZETEKNEV,
                       KERESZTNEV=x.KERESZTNEV,
                       VAROS=x.VAROS,
                        UTCA=x.UTCA,
                        HAZSZAM=x.HAZSZAM,
                        EMAIL=x.EMAIL,
                        TELEFON=x.TELEFON
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

            public bool Előjegyzést_végez(ALLAT allat)
            {
                ALLAT a = db.ALLAT.Find(allat.ALLATID);
                if(a.ELOJEGYZETT==true)
                {
                    return false;
                }
                else
                {
                    a.ELOJEGYZETT =true;
                    Állatot_módosít(a);
                    return true;
                }
                
            }

            public bool Állatot_hozzáad(ALLAT állat)
            {
                try 
                {   
                    db.ALLAT.Add(állat);

                    if (db.SaveChanges() == 0)
                    {
                        return true;
                    }
                    else
                    { return false; }
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

            public bool Állatot_módosít(ALLAT állat)//lecserélem az egészet..id marad
            {
                ALLAT a = db.ALLAT.Find(állat.ALLATID);
                db.ALLAT.Remove(a);
                db.ALLAT.Add(állat);
                if (db.SaveChanges() == 0)
                {
                    return true;
                }
                else
                { return false; }
            }

            public bool Állatot_töröl(ALLAT állat)
            {
                ALLAT a = db.ALLAT.Find(állat);
                db.ALLAT.Remove(a);
                if (db.SaveChanges() == 0)
                {
                    return true;
                }
                else
                { return false; }
            }

            public bool Ügyfelet_hozzáad(UGYFEL ügyfél)
            {
                db.UGYFEL.Add(ügyfél);
                if (db.SaveChanges() == 0)
                {
                    return true;
                }
                else
                { return false; }
            }

            public bool Ügyfelet_töröl(UGYFEL ügyfél)
            {
                var a = db.UGYFEL.Where(x => x.UGYFELID == ügyfél.UGYFELID);
                db.UGYFEL.Remove((UGYFEL)a);
                if (db.SaveChanges() == 0)
                {
                    return true;
                }
                else
                { return false; }
            }

            public bool Ügyfelet_módosít(UGYFEL ügyfél)
            {
                UGYFEL a = db.UGYFEL.Find(ügyfél.UGYFELID);
                db.UGYFEL.Remove(a);
                db.UGYFEL.Add(ügyfél);
                if (db.SaveChanges() == 0)
                {
                    return true;
                }
                else
                { return false; }
            }

            public Statisztika Statisztikát_készít(Statisztika_típus fajta, DateTime idoszak_kezdet, DateTime idoszak_vege)
            {
                Statisztika stat = new Statisztika(fajta, idoszak_kezdet, idoszak_vege);
                stat.MakeStatistic();
               // Task.WaitAll(stat.Tasklist.ToArray());

                List<XElement> elements = new List<XElement>();
                for (int i = 0; i < stat.Napok.Count;i++ )
                {
                    XElement nap = new XElement("Nap");
                    nap.SetAttributeValue("Dátum",stat.Napok[i]);
                    foreach (KeyValuePair<string, List<object>> lista in stat.Stat_listak)
                    {
                        nap.Add(new XElement(lista.Key, lista.Value[i]));
                    }
                    elements.Add(nap);
                }
                   
                //stat.xDoc = new XDocument(fajta.ToString(),new XElement("napok",elements));
                return stat;
            }
        
        }//end Admin_kezelő

    }//end namespace AdatKezelő
