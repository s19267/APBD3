using System;
using System.Collections.Generic;

namespace WebApplication
{
    public partial class Miasto
    {
        public Miasto()
        {
            Osoba = new HashSet<Osoba>();
        }

        public int IdMiasto { get; set; }
        public string Nazwa { get; set; }

        public virtual ICollection<Osoba> Osoba { get; set; }
    }
}
