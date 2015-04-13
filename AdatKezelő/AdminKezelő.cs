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
            public AdatbázisKezelő AdatbázisKezelő;

            public Admin_kezelő()
            {
                AdatbázisKezelő = new AdatbázisKezelő();
            }

            public void Adományoz(Guid ki, string mikor, int mennyit, Adomány_típus mit)
            {
                ADOMANY a = new ADOMANY();
                a.ADOMANYID = Guid.NewGuid();
                a.ADOMANYOZO = ki;
                a.DATUM = DateTime.Now;
                a.MENNYISEG = mennyit;
                a.TIPUS = mit.ToString();
                AdatbázisKezelő.Adományt_hozzáad(a);
            }

            public void Database_communication()//ez miaf...
            {

            }

            public bool Előjegyzést_végez(ALLAT allat)
            {
                if(allat.ELOJEGYZETT==true)
                {
                    return false;
                }
                else
                {
                    allat.ELOJEGYZETT =true;
                    AdatbázisKezelő.Állatot_módosít(allat);
                    return true;
                }
                
            }


            Statisztika IAdmin_kezelő.Statisztikát_készít(string fajta)
            {
                Statisztika stat = new Statisztika();
                return stat;
            }

        }//end Admin_kezelő

    }//end namespace AdatKezelő
