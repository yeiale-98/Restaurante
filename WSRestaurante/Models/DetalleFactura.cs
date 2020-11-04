using System;
using System.Collections.Generic;

namespace WSRestaurante.Models
{
    public partial class DetalleFactura
    {
        public int IdDetalleFactura { get; set; }
        public int NroFactura { get; set; }
        public int IdSupervisor { get; set; }
        public string Plato { get; set; }
        public decimal? Valor { get; set; }

        public virtual Supervisor IdSupervisorNavigation { get; set; }
        public virtual Factura NroFacturaNavigation { get; set; }
    }
}
