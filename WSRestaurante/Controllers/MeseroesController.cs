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
    public class MeseroesController : ControllerBase
    {
        private readonly ComidasTipicasDelSurContext _context;

        public MeseroesController(ComidasTipicasDelSurContext context)
        {
            _context = context;
        }

        // GET: api/Meseroes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mesero>>> GetMesero()
        {
            return await _context.Mesero.ToListAsync();
        }

        // GET: api/Meseroes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mesero>> GetMesero(int id)
        {
            var mesero = await _context.Mesero.FindAsync(id);

            if (mesero == null)
            {
                return NotFound();
            }

            return mesero;
        }

        [HttpGet("[action]/{id}")]
        public bool ValidaMesero(int id)
        {
            return _context.Mesero.Any(e => e.IdMesero == id);
        }

        // PUT: api/Meseroes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMesero(int id, Mesero mesero)
        {
            if (id != mesero.IdMesero)
            {
                return BadRequest();
            }

            _context.Entry(mesero).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeseroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/Meseroes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Mesero>> PostMesero(Mesero mesero)
        {
            _context.Mesero.Add(mesero);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MeseroExists(mesero.IdMesero))
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

        // DELETE: api/Meseroes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Mesero>> DeleteMesero(int id)
        {
            var mesero = await _context.Mesero.FindAsync(id);
            if (mesero == null)
            {
                return NotFound();
            }

            _context.Mesero.Remove(mesero);
            await _context.SaveChangesAsync();

            return mesero;
        }

        private bool MeseroExists(int id)
        {
            return _context.Mesero.Any(e => e.IdMesero == id);
        }
    }
}
