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
    
    public partial class Diagnostice_pacienti
    {
        public int Diagnostice_pacientiID { get; set; }
        public Nullable<int> Pacienti_ID { get; set; }
        public Nullable<int> Diagnostic_ID { get; set; }
        public Nullable<System.DateTime> DataDiagnostic { get; set; }
    
        public virtual Diagnostice Diagnostice { get; set; }
        public virtual Utilizatori Utilizatori { get; set; }
    }
}
