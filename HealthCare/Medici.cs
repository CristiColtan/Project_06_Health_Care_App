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
    
    public partial class Medici
    {
        public int MedicID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Specializare { get; set; }
    
        public virtual Utilizatori Utilizatori { get; set; }
    }
}
