using Microsoft.Office.Interop.Excel;
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
        List<Statisztika_adatrecord> ugyfelek;      
        csillamponiDBEntities db;
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

        public List<Statisztika_adatrecord> Ugyfelek
        {
            get { return ugyfelek; }
            set { ugyfelek = value; }
        }
       
        public Statisztika_típus Tipus
        {
            get { return tipus; }
            set { tipus = value; }
        }
         
//konstruktor   

        public Statisztika() { }

        public Statisztika(Statisztika_típus ujtipus, DateTime ujmettől, DateTime ujmeddig)
        {
            mettől = ujmettől; meddig = ujmeddig; tipus = ujtipus;
            napok = new List<Statisztika_adatrecord>();
            ugyfelek = new List<Statisztika_adatrecord>();
            db = new csillamponiDBEntities();
            tasklist = new List<Task>();
            for (int i = 0; i < (meddig - mettől).TotalDays; i++)
            {
                napok.Add(new Statisztika_adatrecord(mettől.AddDays(i)));
            }
            var clients = db.UGYFEL.Where(x =>(db.ADOMANY.GroupBy(y=>y.ADOMANYOZO).Select(y=>y.Key).ToList()).Contains(x.UGYFELID)).ToList();
            foreach(UGYFEL akt in clients)
            {
                ugyfelek.Add(new Statisztika_adatrecord(akt));
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
                    new XElement("befolyt_penzadomany", akt.befolytPenzadomany_huf),
                    new XElement("befolyt_eledeladomany", akt.befolytEledelAdomany_kg)
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
                        new XElement("befolyt_penzadomany", akt.befolytPenzadomany_huf),
                        new XElement("befolyt_eledeladomany", akt.befolytEledelAdomany_kg),
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
                         select new Statisztika_adatrecord(a.Nap) { befolytPenzadomany_huf= (int)(item1 == null ? 0 : item1.Posszeg), befolytEledelAdomany_kg = (int)(item2 == null ? 0 : item2.Eosszeg) }).ToList();

            }
            else if (tipus == Statisztika_típus.ügyfélállomány)
            {

                var regisztralt = (from a in db.UGYFEL.Where(x => x.REGDATUM >= mettől && x.REGDATUM <= meddig)
                                   group a by new { a.REGDATUM.Value.Year, a.REGDATUM.Value.Month, a.REGDATUM.Value.Day } into g
                                   select new { g.Key.Year, g.Key.Month, g.Key.Day, regisztraltDB = g.Count() }).ToList()
                 .ConvertAll(x => new { date = new DateTime(x.Year, x.Month, x.Day), x.regisztraltDB });

                napok = (from a in napok
                         join b in regisztralt on a.Nap equals b.date into p
                         from item1 in p.DefaultIfEmpty()
                         select new Statisztika_adatrecord(a.Nap) { regisztraltDarab = (int)(item1 == null ? 0 : item1.regisztraltDB) }).ToList();

            }
            else if (tipus == Statisztika_típus.ugyfeladatok)//ide megírom az ügyfelenkénti ugyfelek lista feltöltést
            {
                var penzadomanyai=(from a in db.UGYFEL.Where(x=> x.REGDATUM != null && x.REGDATUM >= mettől && x.REGDATUM <= meddig)
                                   group a by a.UGYFELID into b
                                   select new {id= b.Key,  penz_sum = db.ADOMANY.Where(x=>x.ADOMANYOZO==b.Key && x.TIPUS=="pénz").Sum(x=>x.MENNYISEG)}).ToList();

                var eledeladomanyai=(from a in db.UGYFEL.Where(x=> x.REGDATUM != null && x.REGDATUM >= mettől && x.REGDATUM <= meddig)
                                   group a by a.UGYFELID into b
                                   select new { id=b.Key,  eledel_sum = db.ADOMANY.Where(x=>x.ADOMANYOZO==b.Key && x.TIPUS=="eledel").Sum(x=>x.MENNYISEG)}).ToList();;
              //  var allatai_db=;


                ugyfelek=(from a in ugyfelek
                         join b in penzadomanyai on a.Ugyfel.UGYFELID equals b.id into h
                          join c in eledeladomanyai on a.Ugyfel.UGYFELID equals c.id into y
                           from item1 in h.DefaultIfEmpty()
                         from item2 in y.DefaultIfEmpty()
                          select new Statisztika_adatrecord(a.Ugyfel)
                         {
                             pénztAdomanyozott_huf = (int)(item1 == null ? 0 : item1.penz_sum),
                             eledeltAdomanyozott_kg = (int)(item2 == null ? 0 : item2.eledel_sum)
                         }).ToList();
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

                var regisztralt = (from a in db.UGYFEL.Where(x => x.REGDATUM >= mettől && x.REGDATUM <= meddig)
                                   group a by new { a.REGDATUM.Value.Year, a.REGDATUM.Value.Month, a.REGDATUM.Value.Day } into g
                                   select new { g.Key.Year, g.Key.Month, g.Key.Day, regisztraltDB = g.Count() }).ToList()
                .ConvertAll(x => new { date = new DateTime(x.Year, x.Month, x.Day), x.regisztraltDB });


                napok = (from a in napok
                         join b in elvittDarab on a.Nap equals b.date into h
                         join c in hozottDarab on a.Nap equals c.date into y
                         join f in befolytPenz on a.Nap equals f.date into t
                         join p in befolytEledel on a.Nap equals p.date into l
                         join k in regisztralt on a.Nap equals k.date into e
                         from item1 in h.DefaultIfEmpty()
                         from item2 in y.DefaultIfEmpty()
                         from item3 in t.DefaultIfEmpty()
                         from item4 in l.DefaultIfEmpty()
                         from item5 in e.DefaultIfEmpty()
                         select new Statisztika_adatrecord(a.Nap)
                         {
                             elvittAllat = item1 == null ? 0 : item1.count,
                             hozottAllat = item2 == null ? 0 : item2.count,
                             befolytPenzadomany_huf = (int)(item3 == null ? 0 : item3.Posszeg),
                             befolytEledelAdomany_kg = (int)(item4 == null ? 0 : item4.Eosszeg),
                             regisztraltDarab = (int)(item5 == null ? 0 : item5.regisztraltDB)
                         }).ToList();

            }
        }

        public void ExportToExcel(XDocument xml, string fileName) //Luca
        {
            Application excelapp = new Application();
            Workbook wb = excelapp.Workbooks.Add(Type.Missing);
            try
            {
                if (xml != null)
                {
                    Worksheet ws = wb.Sheets.get_Item(1);

                    int lastUsedColumn = 2;
                    int lastUsedRow = 1;
                   bool first = true;
                    foreach (var day in xml.Element("root").Elements("nap"))
                   {
                        ws.Cells[1, lastUsedColumn] = day.Attribute("datum").Value;
                        if (first)
                        {

                            foreach (var elem in day.Elements())
                            {
                                ws.Cells[++lastUsedRow, 1] = elem.Name.ToString();
                            }
                            first = false;
                        }
                        lastUsedRow = 1;
                        foreach (var item in day.Elements())
                        {
                            ws.Cells[++lastUsedRow, lastUsedColumn] = item.Value;
                        }
                        lastUsedColumn++;
                    }

                   wb.SaveAs(fileName);
                    wb.Close();
               }
                else
                {
                    wb.Close();
                    throw new Exception("Nem sikerült expotrálni az excel fájlt, győződjön meg róla, hogy ki legyen jelölve statisztika tipus!");
                }
            }
            catch (Exception ex)
            {
                wb.Close();
            }
        }
        
    }//end Statisztika

}//end namespace AdatKezelő
