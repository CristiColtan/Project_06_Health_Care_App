//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HealthCare
{
    using System;
    using System.Collections.Generic;
    
    public partial class Analize
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Analize()
        {
            this.Analize_pacienti = new HashSet<Analize_pacienti>();
        }
    
        public string Denumire { get; set; }
        public int AnalizaID { get; set; }
        public string Parametrii { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Analize_pacienti> Analize_pacienti { get; set; }
    }
}
