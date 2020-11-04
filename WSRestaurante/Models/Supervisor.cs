using System;
using System.Collections.Generic;

namespace WSRestaurante.Models
{
    public partial class Supervisor
    {
        public Supervisor()
        {
            DetalleFactura = new HashSet<DetalleFactura>();
        }

        public int IdSupervisor { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int? Edad { get; set; }
        public DateTime? Antiguedad { get; set; }

        public virtual ICollection<DetalleFactura> DetalleFactura { get; set; }
    }
}
