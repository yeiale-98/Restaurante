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
    public class SupervisoresController : ControllerBase
    {
        private readonly ComidasTipicasDelSurContext _context;

        public SupervisoresController(ComidasTipicasDelSurContext context)
        {
            _context = context;
        }

        // GET: api/Supervisores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supervisor>>> GetSupervisor()
        {
            return await _context.Supervisor.ToListAsync();
        }

        // GET: api/Supervisores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Supervisor>> GetSupervisor(int id)
        {
            var supervisor = await _context.Supervisor.FindAsync(id);

            if (supervisor == null)
            {
                return NotFound();
            }

            return supervisor;
        }

        [HttpGet("[action]/{id}")]
        public bool ValidaSupervisor(int id)
        {
            return _context.Supervisor.Any(e => e.IdSupervisor == id);
        }

        // PUT: api/Supervisores/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupervisor(int id, Supervisor supervisor)
        {
            if (id != supervisor.IdSupervisor)
            {
                return BadRequest();
            }

            _context.Entry(supervisor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupervisorExists(id))
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

        // POST: api/Supervisores
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Supervisor>> PostSupervisor(Supervisor supervisor)
        {
            _context.Supervisor.Add(supervisor);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SupervisorExists(supervisor.IdSupervisor))
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

        // DELETE: api/Supervisores/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Supervisor>> DeleteSupervisor(int id)
        {
            var supervisor = await _context.Supervisor.FindAsync(id);
            if (supervisor == null)
            {
                return NotFound();
            }

            _context.Supervisor.Remove(supervisor);
            await _context.SaveChangesAsync();

            return supervisor;
        }

        private bool SupervisorExists(int id)
        {
            return _context.Supervisor.Any(e => e.IdSupervisor == id);
        }
    }
}
