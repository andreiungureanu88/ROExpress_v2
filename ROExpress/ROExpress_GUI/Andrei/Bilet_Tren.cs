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
    
    public partial class Bilet_Tren
    {
        public int ID_Bilet_Tren { get; set; }
        public Nullable<int> ID_Tren { get; set; }
        public string Email { get; set; }
        public int NumarVagon { get; set; }
        public int LocVagon { get; set; }
        public int Clasa { get; set; }
        public System.DateTime DataTren { get; set; }
    
        public virtual Trenuri Trenuri { get; set; }
    }
}
