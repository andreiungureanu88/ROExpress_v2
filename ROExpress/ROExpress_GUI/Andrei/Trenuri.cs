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
    
    public partial class Trenuri
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Trenuri()
        {
            this.Bilet_Tren = new HashSet<Bilet_Tren>();
            this.Statii_Tren = new HashSet<Statii_Tren>();
            this.Vagoane_Tren = new HashSet<Vagoane_Tren>();
        }
    
        public int ID_Tren { get; set; }
        public string Tip_Tren { get; set; }
        public Nullable<int> Viteza_Maxima { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bilet_Tren> Bilet_Tren { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Statii_Tren> Statii_Tren { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vagoane_Tren> Vagoane_Tren { get; set; }
    }
}
