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
            
            if (_context.Caixas.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.Caixas.Add(new Caixa {Dinheiro = 100.00,DadosDeVenda = "PRODUTO 1;CLIENTE JOÃO; QUANTIDADE 1"});
                _context.SaveChanges();
            }
        }

    // GET: 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Caixa>>> GetCaixas()
    {
        return await _context.Caixas.ToListAsync();
    }

    // GET with id: 
    [HttpGet("{id}")]
    public async Task<ActionResult<Caixa>> GetCaixas(int id)
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
    public async Task<IActionResult> PutCaixas(long id, Operador item)
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
    public async Task<IActionResult> DeleteTodoItem(int id)
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