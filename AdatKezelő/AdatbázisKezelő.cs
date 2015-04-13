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

	public class AdatbázisKezelő 
    {
        csillamponimenhelyDBEntities db;
        Random r;
        public AdatbázisKezelő() 
        { 
            db = new csillamponimenhelyDBEntities();
            r = new Random();
            Init_db();
        }
		

         public void Init_db()
         {
             for(int i=0;i<100; i++)
             {
                 ALLAT a = new ALLAT();
                 UGYFEL u = new UGYFEL();
                 ADOMANY ad = new ADOMANY();

                 a.ALLATID = Guid.NewGuid();
                 u.UGYFELID = Guid.NewGuid();
                 ad.ADOMANYID = Guid.NewGuid();
                 

             }
         }

		public bool Adományt_hozzáad( ADOMANY ad)
        {
            db.ADOMANY.Add(ad);
            if (db.SaveChanges() == 0)
            {
                return true;
            }
            else
            { return false;}
			
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

        public bool Állatot_módosít(ALLAT állat)
        {
            ALLAT a = db.ALLAT.Find(állat.ALLATID);
           
            if (db.SaveChanges() == 0)
            {
                return true;
            }
            else
            { return false; }
        }

        public bool Állatot_töröl(ALLAT állat)
        {
            var a = db.ALLAT.Where(x=>x.ALLATID==állat.ALLATID);
            db.ALLAT.Remove((ALLAT)a);
            if (db.SaveChanges() == 0)
            {
                return true;
            }
            else
            { return false; }
		}

		public bool Connect_To_Db()//itt kéne bejelentkeznie?
        {
           
            return true;
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

	}//end AdatbázisKezelő

}//end namespace AdatKezelő

