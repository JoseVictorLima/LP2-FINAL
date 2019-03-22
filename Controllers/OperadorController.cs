using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MercadoApi.Models;

namespace MercadoApi.Controllers
{
    [Route("mercado/operadores")]
    [ApiController]
    public class OperadorController : ControllerBase
    {
        private readonly Context _context;

        public OperadorController(Context context)
        {
            _context = context;
            
            if (_context.Operadores.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.Operadores.Add(new Operador {Funcionario = new Funcionario{Nome = "Operador", Cpf = "2",Sexo = "M", Turno = "Manhã,Tarde", Salario =  1500.50}});
                _context.SaveChanges();
            }
        }

    // GET: 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Operador>>> GetOperadores()
    {
        return await _context.Operadores.ToListAsync();
    }

    // GET with id: 
    [HttpGet("{id}")]
    public async Task<ActionResult<Operador>> GetOperadores(int id)
    {
        var gerente = await _context.Operadores.FindAsync(id);

        if (gerente == null)
        {
            return NotFound();
        }

        return gerente;
    }

    // POST: 
    [HttpPost]
    public async Task<ActionResult<Operador>> PostOperadores(Operador item)
    {
        _context.Operadores.Add(item);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOperadores), new { id = item.Id }, item);
    }

    // PUT: 
    [HttpPut("{id}")]
    public async Task<IActionResult> PutOperadores(long id, Operador item)
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
        var gerente = await _context.Operadores.FindAsync(id);

        if (gerente == null)
        {
            return NotFound();
        }

        _context.Operadores.Remove(gerente);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    }
}