using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdatKezelő
{
   public class AllatVM
    {
        public System.Guid ALLATID { get; set; }
        public string FAJTA { get; set; }
        public string NEV { get; set; }
        public Nullable<System.DateTime> SZULETESI_IDO { get; set; }
        public Nullable<bool> IVARTALANITOTT { get; set; }
        public string SZIN { get; set; }
        public Nullable<bool> OLTVA { get; set; }
        public string BETEGSEGEK { get; set; }
        public Nullable<bool> NOSTENY { get; set; }
        public Nullable<decimal> TOMEG { get; set; }
        public Nullable<decimal> MERET { get; set; }
        public Nullable<bool> CHIPES { get; set; }
        public Nullable<bool> ELOJEGYZETT { get; set; }
        public Nullable<System.Guid> ELOZO_TULAJ { get; set; }
        public Nullable<System.DateTime> OROKBEFOGADVA { get; set; }
        public Nullable<System.DateTime> BEADVA { get; set; }
    }
}
