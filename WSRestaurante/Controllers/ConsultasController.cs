﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WSRestaurante.Models;

namespace WSRestaurante.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasController : ControllerBase
    {
        private readonly ComidasTipicasDelSurContext _context;

        public ConsultasController(ComidasTipicasDelSurContext context)
        {
            _context = context;
        }

        [HttpGet("VentasMeseros")]
        public async Task<IEnumerable<object>> VentasMeseros(string fechaInicial, string fechaFinal)
        {
            DateTime fecInicial, fecFin;
            fecInicial = DateTime.Parse(fechaInicial);
            fecFin = DateTime.Parse(fechaFinal);

            var resultado = await (from m in _context.Mesero
                                    join f in _context.Factura on m.IdMesero equals f.IdMesero into MeseroFactura
                                    from mf in MeseroFactura.DefaultIfEmpty()
                                    group m by new { m.IdMesero, m.Nombres, m.Apellidos } into grupo
                                    select new
                                    {
                                        grupo.Key.IdMesero,
                                        grupo.Key.Nombres,
                                        grupo.Key.Apellidos,
                                        Valor = (from dt in _context.DetalleFactura
                                                 join ft in _context.Factura on dt.NroFactura equals ft.NroFactura
                                                 where grupo.Key.IdMesero == ft.IdMesero && ft.Fecha >= fecInicial && ft.Fecha <= fecFin
                                                 select dt.Valor ?? 0).Sum()
                                    }).ToListAsync();

            return resultado.OrderBy(x => x.Valor).ToList();
        }

        [HttpGet("ConsumosClientes")]
        public async Task<IEnumerable<object>> ConsumosClientes(int valor)
        {
            var resultado = await (from c in _context.Cliente
                                   join f in _context.Factura on c.Identificacion equals f.IdCliente into ClienteFactura
                                   from cf in ClienteFactura.DefaultIfEmpty()
                                   join df in _context.DetalleFactura on cf.NroFactura equals df.NroFactura into FacturaDetalle
                                   from fd in FacturaDetalle.DefaultIfEmpty()
                                   where fd.Valor >= valor
                                   select new
                                   {
                                       cf.NroFactura,
                                       c.Identificacion,
                                       c.Nombres,
                                       c.Apellidos,
                                       fd.Plato,
                                       fd.Valor
                                   }).ToListAsync();

            return resultado.OrderBy(x => x.Valor).ToList();
        }

        [HttpGet("TopProductos")]
        public async Task<object> TopProductos(string fechaInicial, string fechaFinal)
        {
            DateTime fecInicial = DateTime.Parse(fechaInicial);
            DateTime fecFin = DateTime.Parse(fechaFinal);

            var resultado = await (from df in _context.DetalleFactura
                                   join f in _context.Factura on df.NroFactura equals f.NroFactura
                                   where f.Fecha >= fecInicial & f.Fecha <= fecFin
                                   group df by new { df.Plato, df.Valor } into g
                                   select new
                                   {
                                       CantPlatos = g.Count(),
                                       g.Key.Plato,
                                       Valor = g.Sum(x => x.Valor ?? 0)
                                   }).ToListAsync();

            return resultado.OrderByDescending(x => x.CantPlatos).FirstOrDefault();

        }
    }
}
