using System;
using System.Collections.Generic;

namespace WSRestaurante.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Factura = new HashSet<Factura>();
        }

        public int Identificacion { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public int? Telefono { get; set; }

        public virtual ICollection<Factura> Factura { get; set; }
    }
}
