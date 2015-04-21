using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdatKezelő
{
    public class UgyfelVM
    {
        public string VEZETEKNEV { get; set; }
        public string KERESZTNEV { get; set; }
       
        public string VAROS { get; set; }
        public string UTCA { get; set; }
        public Nullable<decimal> HAZSZAM { get; set; }
        public Nullable<decimal> TELEFON { get; set; }
        public System.Guid UGYFELID { get; set; }
        public string EMAIL { get; set; }
    }
}
