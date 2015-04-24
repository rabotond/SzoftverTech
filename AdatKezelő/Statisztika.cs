
//using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;


namespace AdatKezelő
{
    public class Statisztika
    {
//adattagok
        private DateTime mettől;
        private DateTime meddig;
        private Statisztika_típus tipus;
        List<Statisztika_adatrecord> napok; //ide fog elkészülni az egész statisztika, minden elem, egy adatsor, a típus szerint lekért stzatisztikai információkkal
        csillamponimenhelyDBEntities db;
        List<Task> tasklist;
        XDocument xdoc;

//tulajdonságok

        public XDocument xDoc
        {
            get { return xdoc; }
            set { xdoc = value; }
        }

        public List<Statisztika_adatrecord> Napok
        {
            get { return napok; }
            set { napok = value; }
        }

        public DateTime Meddig
        {
            get { return meddig; }
            set { meddig = value; }
        }

        public DateTime Mettől
        {
            get { return mettől; }
            set { mettől = value; }
        }

        public Statisztika_típus Tipus
        {
            get { return tipus; }
            set { tipus = value; }
        }
 
        
//konstruktor   

        public Statisztika()
        {

        }

        public Statisztika(Statisztika_típus ujtipus, DateTime ujmettől, DateTime ujmeddig)
        {
            mettől = ujmettől; meddig = ujmeddig; tipus = ujtipus;
            napok = new List<Statisztika_adatrecord>();
            db = new csillamponimenhelyDBEntities();
            tasklist = new List<Task>();
            for (int i = 0; i < (meddig - mettől).TotalDays; i++)
            {
                napok.Add(new Statisztika_adatrecord(mettől.AddDays(i)));
            }
            MakeStatistic();
        }

// stat készítő eljárás

        public void makeXmlfromStat()
        {

            XElement root = new XElement("root");
            xdoc = new XDocument(root);
            foreach (Statisztika_adatrecord akt in napok)
            {
                XElement e;
                if (tipus == Statisztika_típus.állatállomány)
                {
                        e = new XElement("nap",
                    new XAttribute("datum", akt.Nap),
                    new XElement("elvitt_allat", akt.elvittAllat),
                    new XElement("hozott_allat", akt.hozottAllat)
                      );
                }
                else if (tipus == Statisztika_típus.adomány)
                {
                        e = new XElement("nap",
                    new XAttribute("datum", akt.Nap),
                    new XElement("befolyt_penzadomany", akt.befolytPenzadomany),
                    new XElement("befolyt_eledeladomany", akt.befolytEledelAdomany)
                      );
                }
                else if (tipus == Statisztika_típus.ügyfélállomány)
                {
                        e = new XElement("nap",
                        new XAttribute("datum",akt.Nap),
                        new XElement("regisztraltak",akt.regisztraltDarab)
                      );
                }
                else
                {  
                    e = new XElement("nap",
                    new XAttribute("datum", akt.Nap),
                    new XElement("elvitt_allat", akt.elvittAllat),
                    new XElement("hozott_allat", akt.hozottAllat),
                        new XElement("befolyt_penzadomany", akt.befolytPenzadomany),
                        new XElement("befolyt_eledeladomany", akt.befolytEledelAdomany),
                        new XElement("regisztraltak", akt.regisztraltDarab)
                        );
                }

                root.Add(e);
            }


        }


        public void MakeStatistic()// a convertAll azért kellett, mert select után névtelen osztály felépítésénél nem adhatok meg oylan adattagot, 
                                     //aminek a konstruktorában még paramétert is rakok, viszont a "date" adattag az ilyen, mert 3 elemből építettem fel
        {

            if (tipus == Statisztika_típus.állatállomány)
            {
                var elvittDarab = (from a in db.ALLAT.Where(x => x.OROKBEFOGADVA != null && x.OROKBEFOGADVA >= mettől && x.OROKBEFOGADVA <= meddig)
                            group a by new { a.OROKBEFOGADVA.Value.Year, a.OROKBEFOGADVA.Value.Month, a.OROKBEFOGADVA.Value.Day } into g
                            select new { g.Key.Year, g.Key.Month, g.Key.Day, count = g.Count() }).ToList()
                       .ConvertAll(x => new { date = new DateTime(x.Year, x.Month, x.Day), x.count });

                var hozottDarab = (from a in db.ALLAT.Where(x => x.BEADVA != null && x.BEADVA >= mettől && x.BEADVA <= meddig)
                                   group a by new { a.BEADVA.Value.Year, a.BEADVA.Value.Month, a.BEADVA.Value.Day } into g
                                   select new { g.Key.Year, g.Key.Month, g.Key.Day, count = g.Count() }).ToList()
                     .ConvertAll(x => new { date = new DateTime(x.Year, x.Month, x.Day), x.count });


                napok = (from a in napok
                         join b in elvittDarab on a.Nap equals b.date into e
                         join c in hozottDarab on a.Nap equals c.date into h
                         from item1 in e.DefaultIfEmpty()
                         from item2 in h.DefaultIfEmpty()
                         select new Statisztika_adatrecord(a.Nap) { elvittAllat = item1 == null ? 0 : item1.count, hozottAllat = item2 == null ? 0 : item2.count }).ToList();
            }

            else if (tipus == Statisztika_típus.adomány)
            {

                var befolytPenz = (from a in db.ADOMANY.Where(x => x.TIPUS == "PÉNZ" && x.DATUM >= mettől && x.DATUM <= meddig)
                                   group a by new { a.DATUM.Value.Year, a.DATUM.Value.Month, a.DATUM.Value.Day } into g
                                   select new { g.Key.Year, g.Key.Month, g.Key.Day, Posszeg = g.Sum(x => x.MENNYISEG) }).ToList()
                   .ConvertAll(x => new { date = new DateTime(x.Year, x.Month, x.Day), x.Posszeg });

                var befolytEledel = (from a in db.ADOMANY.Where(x => x.TIPUS == "ELEDEL" && x.DATUM >= mettől && x.DATUM <= meddig)
                                     group a by new { a.DATUM.Value.Year, a.DATUM.Value.Month, a.DATUM.Value.Day } into g
                                     select new { g.Key.Year, g.Key.Month, g.Key.Day, Eosszeg = g.Sum(x => x.MENNYISEG) }).ToList()
                   .ConvertAll(x => new { date = new DateTime(x.Year, x.Month, x.Day), x.Eosszeg });

                napok = (from a in napok
                         join b in befolytPenz on a.Nap equals b.date into p
                         join c in befolytEledel on a.Nap equals c.date into e
                         from item1 in p.DefaultIfEmpty()
                         from item2 in e.DefaultIfEmpty()
                         select new Statisztika_adatrecord(a.Nap) { befolytPenzadomany = (int)(item1 == null ? 0 : item1.Posszeg), befolytEledelAdomany = (int)(item2 == null ? 0 : item2.Eosszeg) }).ToList();

            }
            else if (tipus == Statisztika_típus.ügyfélállomány)
            {

                var regisztralt = (from a in db.UGYFEL.Where(x => x.REGDATUM >= mettől && x.REGDATUM <= meddig)
                                   group a by new { a.REGDATUM.Value.Year, a.REGDATUM.Value.Month, a.REGDATUM.Value.Day } into g
                                   select new { g.Key.Year, g.Key.Month, g.Key.Day, regisztraltDB = g.Count() }).ToList()
                 .ConvertAll(x => new { date = new DateTime(x.Year, x.Month, x.Day), x.regisztraltDB });

                //var tamogatas_menny=(from ado in db.ADOMANY join ugyf in db.UGYFEL on ado.ADOMANYOZO equals ugyf.UGYFELID into v
                //                             from item1 in v
                //                         select new{ ugyfel=item1.UGYFELID, mit=item1.ADOMANY.}).ToList();
                ////var tamogatasa = (from a in db.UGYFEL.Where(x => x.regisztracio >= mettől && x.regisztracio <= meddig)
                                   
                //                  group a by new { a.regisztracio.Value.Year, a.regisztracio.Value.Month, a.regisztracio.Value.Day ,a.UGYFELID } into g
                //                  select new { g.Key.Year, g.Key.Month, g.Key.Day,  tamogatott =where g.Key.UGYFELID==d ,id=g.Key.UGYFELID
                //                  }).ToList()
                // .ConvertAll(x => new { date = new DateTime(x.Year, x.Month, x.Day), x.tamogatott});

                napok = (from a in napok
                         join b in regisztralt on a.Nap equals b.date into p
                         from item1 in p.DefaultIfEmpty()
                         select new Statisztika_adatrecord(a.Nap) { regisztraltDarab = (int)(item1 == null ? 0 : item1.regisztraltDB) }).ToList();

            }
            else//összetettbe mindent
            {
                var elvittDarab = (from a in db.ALLAT.Where(x => x.OROKBEFOGADVA != null && x.OROKBEFOGADVA >= mettől && x.OROKBEFOGADVA <= meddig)
                                   group a by new { a.OROKBEFOGADVA.Value.Year, a.OROKBEFOGADVA.Value.Month, a.OROKBEFOGADVA.Value.Day } into g
                                   select new { g.Key.Year, g.Key.Month, g.Key.Day, count = g.Count() }).ToList()
                      .ConvertAll(x => new { date = new DateTime(x.Year, x.Month, x.Day), x.count });

                var hozottDarab = (from a in db.ALLAT.Where(x => x.BEADVA != null && x.BEADVA >= mettől && x.BEADVA <= meddig)
                                   group a by new { a.BEADVA.Value.Year, a.BEADVA.Value.Month, a.BEADVA.Value.Day } into g
                                   select new { g.Key.Year, g.Key.Month, g.Key.Day, count = g.Count() }).ToList()
                     .ConvertAll(x => new { date = new DateTime(x.Year, x.Month, x.Day), x.count });

                var befolytPenz = (from a in db.ADOMANY.Where(x => x.TIPUS == "PÉNZ" && x.DATUM >= mettől && x.DATUM <= meddig)
                                   group a by new { a.DATUM.Value.Year, a.DATUM.Value.Month, a.DATUM.Value.Day } into g
                                   select new { g.Key.Year, g.Key.Month, g.Key.Day, Posszeg = g.Sum(x => x.MENNYISEG) }).ToList()
                  .ConvertAll(x => new { date = new DateTime(x.Year, x.Month, x.Day), x.Posszeg });

                var befolytEledel = (from a in db.ADOMANY.Where(x => x.TIPUS == "ELEDEL" && x.DATUM >= mettől && x.DATUM <= meddig)
                                     group a by new { a.DATUM.Value.Year, a.DATUM.Value.Month, a.DATUM.Value.Day } into g
                                     select new { g.Key.Year, g.Key.Month, g.Key.Day, Eosszeg = g.Sum(x => x.MENNYISEG) }).ToList()
                   .ConvertAll(x => new { date = new DateTime(x.Year, x.Month, x.Day), x.Eosszeg });

                //napok = (from a in napok
                //         join b in regisztralt on a.Nap equals b.date into p
                //         join c in befolytEledel on a.Nap equals c.date into e
                //         from item1 in p.DefaultIfEmpty()
                //         from item2 in e.DefaultIfEmpty()
                //         select new Statisztika_adatrecord(a.Nap) { befolytPenzaomany = (int)(item1 == null ? 0 : item1.Posszeg), befolytEledelAdomany = (int)(item2 == null ? 0 : item2.Eossze) }).ToList();
            
            }
        }

        //public void ExportToExcel(XDocument xml, string fileName) //Luca
        //{
        //    Application excelapp = new Application();
        //    Workbook wb = excelapp.Workbooks.Add(Type.Missing);
        //    try
        //    {
        //        if (xml != null)
        //        {
        //            Worksheet ws = wb.Sheets.get_Item(1);

        //            int lastUsedColumn = 2;
        //            int lastUsedRow = 1;
        //            bool first = true;
        //            foreach (var day in xml.Element("root").Elements("nap"))
        //            {
        //                ws.Cells[1, lastUsedColumn] = day.Attribute("datum").Value;
        //                if (first)
        //                {

        //                    foreach (var elem in day.Elements())
        //                    {
        //                        ws.Cells[++lastUsedRow, 1] = elem.Name.ToString();
        //                    }
        //                    first = false;
        //                }
        //                lastUsedRow = 1;
        //                foreach (var item in day.Elements())
        //                {
        //                    ws.Cells[++lastUsedRow, lastUsedColumn] = item.Value;
        //                }
        //                lastUsedColumn++;
        //            }

        //            wb.SaveAs(fileName);
        //            wb.Close();
        //        }
        //        else
        //        {
        //            wb.Close();
        //            throw new Exception("Nem sikerült expotrálni az excel fájlt, győződjön meg róla, hogy ki legyen jelölve statisztika tipus!");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        wb.Close();
        //    }
        //}
        
    }//end Statisztika

}//end namespace AdatKezelő
