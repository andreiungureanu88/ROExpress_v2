//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ROExpress_GUI.Andrei
{
    using System;
    using System.Collections.Generic;
    
    public partial class Statii_Tren
    {
        public int ID_Statie_Tren { get; set; }
        public Nullable<int> ID_Statie { get; set; }
        public Nullable<int> ID_Tren { get; set; }
        public System.TimeSpan Ora_Plecare { get; set; }
        public System.TimeSpan Ora_Sosire { get; set; }
    
        public virtual Statii Statii { get; set; }
        public virtual Trenuri Trenuri { get; set; }
    }
}