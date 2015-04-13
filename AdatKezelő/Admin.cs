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
    public class Admin
    {
        public Admin_kezelő adminkezelo;
        string jelszó;
        string név;

        public Admin()
        {
            adminkezelo = new Admin_kezelő();
        }

    }//end Admin

}//end namespace AdatKezelő
