//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdatKezelő
{
    using System;
    using System.Collections.Generic;
    
    public partial class ALLAT
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
        public Nullable<int> TOMEG { get; set; }
        public Nullable<int> MERET { get; set; }
        public Nullable<bool> CHIPES { get; set; }
        public Nullable<bool> ELOJEGYZETT { get; set; }
        public Nullable<System.Guid> ELOZO_TULAJ { get; set; }
        public string KEP { get; set; }
        public Nullable<System.DateTime> OROKBEFOGADVA { get; set; }
        public Nullable<System.DateTime> BEADVA { get; set; }
    
        public virtual KENNEL KENNEL { get; set; }
        public virtual UGYFEL UGYFEL { get; set; }
    }
}
