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

            public csillamponimenhelyDBEntities Db
            {
                get { return db; }
                set { db = value; }
            }

           public List<ALLAT> getAllAllat()
            {
                return db.ALLAT.Where(x=>x.ALLATID!=null && x.NEV!=null).ToList();
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
                Statisztika stat = new Statisztika( fajta,  idoszak_kezdet,  idoszak_vege);
                return stat;
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
        }//end Admin_kezelő

    }//end namespace AdatKezelő
