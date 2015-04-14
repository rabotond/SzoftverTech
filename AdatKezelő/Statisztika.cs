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
    public class Statisztika
    {

        private DateTime meddig;
        private DateTime mettől;
        private string név;
        private string tipus;
        public Admin_kezelő m_Admin_kezelő;

        public Statisztika()
        {
            //asdsasdasd
        }

        ~Statisztika()
        {

        }

        public virtual void Dispose()
        {

        }

        /// 
        /// <param name="dátum"></param>
        /// <param name="tipus"></param>
        /// <param name="név"></param>
        public Statisztika(string dátum, string tipus, string név)
        {

        }

        public void Nyomtat()
        {

        }

    }//end Statisztika

}//end namespace AdatKezelő
