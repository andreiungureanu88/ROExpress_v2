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
    
    public partial class Statii
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Statii()
        {
            this.Calatoriis = new HashSet<Calatorii>();
            this.Statii_Tren = new HashSet<Statii_Tren>();
        }
    
        public int ID_Statie { get; set; }
        public string Nume_Oras { get; set; }
        public double Latitudine { get; set; }
        public double Longitudine { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Calatorii> Calatoriis { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Statii_Tren> Statii_Tren { get; set; }
    }
}
