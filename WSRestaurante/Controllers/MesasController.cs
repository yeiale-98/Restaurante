﻿using System;
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
    public class MesasController : ControllerBase
    {
        private readonly ComidasTipicasDelSurContext _context;

        public MesasController(ComidasTipicasDelSurContext context)
        {
            _context = context;
        }

        // GET: api/Mesas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mesa>>> GetMesa()
        {
            return await _context.Mesa.ToListAsync();
        }

        // GET: api/Mesas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mesa>> GetMesa(int id)
        {
            var mesa = await _context.Mesa.FindAsync(id);

            if (mesa == null)
            {
                return NotFound();
            }

            return mesa;
        }

        [HttpGet("[action]/{id}")]
        public bool ValidaMesa(int id)
        {
            return _context.Mesa.Any(e => e.NroMesa == id);
        }

        // PUT: api/Mesas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMesa(int id, Mesa mesa)
        {
            if (id != mesa.NroMesa)
            {
                return BadRequest();
            }

            _context.Entry(mesa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MesaExists(id))
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

        // POST: api/Mesas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Mesa>> PostMesa(Mesa mesa)
        {
            _context.Mesa.Add(mesa);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MesaExists(mesa.NroMesa))
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

        // DELETE: api/Mesas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Mesa>> DeleteMesa(int id)
        {
            var mesa = await _context.Mesa.FindAsync(id);
            if (mesa == null)
            {
                return NotFound();
            }

            _context.Mesa.Remove(mesa);
            await _context.SaveChangesAsync();

            return mesa;
        }

        private bool MesaExists(int id)
        {
            return _context.Mesa.Any(e => e.NroMesa == id);
        }
    }
}
