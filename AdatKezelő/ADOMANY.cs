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
    
    public partial class ADOMANY
    {
        public System.Guid ADOMANYID { get; set; }
        public string TIPUS { get; set; }
        public System.Guid ADOMANYOZO { get; set; }
        public Nullable<decimal> MENNYISEG { get; set; }
        public System.DateTime DATUM { get; set; }
    
        public virtual UGYFEL UGYFEL { get; set; }
    }
}
