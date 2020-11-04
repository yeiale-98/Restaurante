using System;
using System.Collections.Generic;

namespace WSRestaurante.Models
{
    public partial class Factura
    {
        public int NroFactura { get; set; }
        public int IdCliente { get; set; }
        public int NroMesa { get; set; }
        public int IdMesero { get; set; }
        public DateTime Fecha { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; }
        public virtual Mesero IdMeseroNavigation { get; set; }
        public virtual Mesa NroMesaNavigation { get; set; }
        public virtual DetalleFactura DetalleFactura { get; set; }
    }
}
