//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WorkControlPortal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Departmanlar
    {
        public Departmanlar()
        {
            this.Cariler = new HashSet<Cariler>();
            this.Hizmetler = new HashSet<Hizmetler>();
            this.Kullanicilar = new HashSet<Kullanicilar>();
        }
    
        public int Referans { get; set; }
        public string Departman { get; set; }
    
        public virtual ICollection<Cariler> Cariler { get; set; }
        public virtual ICollection<Hizmetler> Hizmetler { get; set; }
        public virtual ICollection<Kullanicilar> Kullanicilar { get; set; }
    }
}
