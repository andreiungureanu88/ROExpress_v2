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
    
    public partial class Vagoane_Tren
    {
        public int ID_Vagoane_Tren { get; set; }
        public Nullable<int> ID_Tren { get; set; }
        public Nullable<int> ID_Vagon { get; set; }
        public int Nr_Vagon { get; set; }
    
        public virtual Tipuri_Vagoane Tipuri_Vagoane { get; set; }
        public virtual Trenuri Trenuri { get; set; }
    }
}
