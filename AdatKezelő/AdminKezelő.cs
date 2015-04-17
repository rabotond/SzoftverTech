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
        public class Admin_kezelő : IAdmin_kezelő
        {
            csillamponimenhelyDBEntities db;

            public csillamponimenhelyDBEntities Db
            {
                get { return db; }
                set { db = value; }
            }

           public List<ALLAT> getAllAllat()
            {
                return db.ALLAT.Where(x=>x.ALLATID!=null).ToList();
            }

           public List<UGYFEL> getAllÜgyfél()
           {
               return db.UGYFEL.Where(x => x.UGYFELID != null).ToList();
           }


            public Admin_kezelő()
            {
                db=new csillamponimenhelyDBEntities();
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

            public Statisztika Statisztikát_készít(string fajta, DateTime idoszak_kezdet, DateTime idoszak_vege)
            {
                Statisztika stat = new Statisztika();
                return stat;
            }

            public bool Állatot_hozzáad(ALLAT állat)
            {
                db.ALLAT.Add(állat);
                if (db.SaveChanges() == 0)
                {
                    return true;
                }
                else
                { return false; }
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
                var a = db.ALLAT.Where(x => x.ALLATID == állat.ALLATID);
                db.ALLAT.Remove((ALLAT)a);
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
        }//end Admin_kezelő

    }//end namespace AdatKezelő
