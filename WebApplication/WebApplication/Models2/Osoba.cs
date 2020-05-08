using System;
using System.Collections.Generic;

namespace WebApplication
{
    public partial class Osoba
    {
        public int IdOsoby { get; set; }
        public string Nazwisko { get; set; }
        public DateTime? DataUrodzenia { get; set; }
        public string Adres { get; set; }
        public int? IdMiasto { get; set; }
        public string Zawod { get; set; }

        public virtual Miasto IdMiastoNavigation { get; set; }
    }
}
