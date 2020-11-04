using System;
using System.Collections.Generic;

namespace WSRestaurante.Models
{
    public partial class Mesero
    {
        public Mesero()
        {
            Factura = new HashSet<Factura>();
        }

        public int IdMesero { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int? Edad { get; set; }
        public DateTime? Antiguedad { get; set; }

        public virtual ICollection<Factura> Factura { get; set; }
    }
}
