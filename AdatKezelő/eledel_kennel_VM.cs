using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdatKezelő
{
    public class eledel_kennel_VM
    {
        
        public string TIPUS { get; set; }
        public int RAKTARON_kg { get; set; }
        
        public Nullable<int> SZABAD_kennelek { get; set; }
        public Nullable<int> FOGLALT_kennelek { get; set; }
        public int MAXkennel { get; set; }
    }
}
