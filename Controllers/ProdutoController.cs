using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MercadoApi.Models;

namespace MercadoApi.Controllers
{
    [Route("mercado/produtos")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly Context _context;

        public ProdutoController(Context context)
        {
            _context = context;
            
            if (_context.Produtos.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.Produtos.Add(new Produto {Codigo = "1",Nome = "Produto",Preco = 10.00,Quantidade = 1,Peso = 1.00});
                _context.SaveChanges();
            }
        }

    // GET: 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
    {
        return await _context.Produtos.ToListAsync();
    }

    // GET with id: 
    [HttpGet("{id}")]
    public async Task<ActionResult<Produto>> GetProdutos(int id)
    {
        var gerente = await _context.Produtos.FindAsync(id);

        if (gerente == null)
        {
            return NotFound();
        }

        return gerente;
    }

    // POST: 
    [HttpPost]
    public async Task<ActionResult<Produto>> PostProdutos(Produto item)
    {
        _context.Produtos.Add(item);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProdutos), new { id = item.Id }, item);
    }

    // PUT: 
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProdutos(long id, Produto item)
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
        var gerente = await _context.Produtos.FindAsync(id);

        if (gerente == null)
        {
            return NotFound();
        }

        _context.Produtos.Remove(gerente);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    }
}