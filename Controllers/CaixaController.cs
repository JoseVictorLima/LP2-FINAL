using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MercadoApi.Models;

namespace MercadoApi.Controllers
{
    [Route("mercado/caixas")]
    [ApiController]
    public class CaixaController : ControllerBase
    {
        private readonly Context _context;

        public CaixaController(Context context)
        {
            _context = context;
        }

    // GET: 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Caixa>>> GetCaixas()
    {
        return await _context.Caixas.ToListAsync();
    }

    // GET with id: 
    [HttpGet("{id}")]
    public async Task<ActionResult<Caixa>> GetCaixa(int id)
    {
        var caixa = await _context.Caixas.FindAsync(id);

        if (caixa == null)
        {
            return NotFound();
        }

        return caixa;
    }

    // POST: 
    [HttpPost]
    public async Task<ActionResult<Caixa>> PostCaixas(Caixa item)
    {
        _context.Caixas.Add(item);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCaixas), new { id = item.Id }, item);
    }

    // PUT: 
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCaixa(int id, Caixa item)
    {
        if (id != item.Id)
        {
            return BadRequest();
        }

        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE with id:
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCaixa(int id)
    {
        var caixa = await _context.Caixas.FindAsync(id);

        if (caixa == null)
        {
            return NotFound();
        }

        _context.Caixas.Remove(caixa);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    }
}