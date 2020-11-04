using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WSRestaurante.Models;

namespace WSRestaurante.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleFacturasController : ControllerBase
    {
        private readonly ComidasTipicasDelSurContext _context;

        public DetalleFacturasController(ComidasTipicasDelSurContext context)
        {
            _context = context;
        }

        // GET: api/DetalleFacturas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleFactura>>> GetDetalleFactura()
        {
            return await _context.DetalleFactura.ToListAsync();
        }

        // GET: api/DetalleFacturas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleFactura>> GetDetalleFactura(int id)
        {
            var detalleFactura = await _context.DetalleFactura.FindAsync(id);

            if (detalleFactura == null)
            {
                return NotFound();
            }

            return detalleFactura;
        }

        // PUT: api/DetalleFacturas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleFactura(int id, DetalleFactura detalleFactura)
        {
            if (id != detalleFactura.IdDetalleFactura)
            {
                return BadRequest();
            }

            _context.Entry(detalleFactura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleFacturaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/DetalleFacturas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DetalleFactura>> PostDetalleFactura(DetalleFactura detalleFactura)
        {
            detalleFactura.IdDetalleFactura = (UltimoConsecutivo() + 1);
            _context.DetalleFactura.Add(detalleFactura);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DetalleFacturaExists(detalleFactura.IdDetalleFactura))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // DELETE: api/DetalleFacturas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DetalleFactura>> DeleteDetalleFactura(int id)
        {
            var detalleFactura = await _context.DetalleFactura.FindAsync(id);
            if (detalleFactura == null)
            {
                return NotFound();
            }

            _context.DetalleFactura.Remove(detalleFactura);
            await _context.SaveChangesAsync();

            return detalleFactura;
        }

        private bool DetalleFacturaExists(int id)
        {
            return _context.DetalleFactura.Any(e => e.IdDetalleFactura == id);
        }

        private int UltimoConsecutivo()
        {
            return _context.DetalleFactura.Max(e => e.IdDetalleFactura);
        }
    }
}
