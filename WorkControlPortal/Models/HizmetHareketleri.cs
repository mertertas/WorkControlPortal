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
    
    public partial class HizmetHareketleri
    {
        public int Referans { get; set; }
        public Nullable<System.DateTime> BaTarih { get; set; }
        public Nullable<System.DateTime> BiTarih { get; set; }
        public Nullable<int> ProjeRef { get; set; }
        public Nullable<int> HizmetRef { get; set; }
        public Nullable<int> CariRef { get; set; }
        public string Ilgili { get; set; }
        public string Aciklama { get; set; }
        public Nullable<int> DprtRef { get; set; }
        public Nullable<int> Ekleyen { get; set; }
        public Nullable<bool> Ucret { get; set; }
        public Nullable<bool> Kapama { get; set; }
        public Nullable<bool> MailBil { get; set; }
        public Nullable<int> Zaman { get; set; }
        public Nullable<int> Hafta { get; set; }
    
        public virtual Cariler Cariler { get; set; }
        public virtual Hizmetler Hizmetler { get; set; }
        public virtual Kullanicilar Kullanicilar { get; set; }
        public virtual Projeler Projeler { get; set; }
    }
}
