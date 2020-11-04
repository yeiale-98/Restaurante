using System;
using System.Collections.Generic;

namespace WSRestaurante.Models
{
    public partial class Mesa
    {
        public Mesa()
        {
            Factura = new HashSet<Factura>();
        }

        public int NroMesa { get; set; }
        public bool? Reservada { get; set; }
        public int? Puestos { get; set; }

        public virtual ICollection<Factura> Factura { get; set; }
    }
}
