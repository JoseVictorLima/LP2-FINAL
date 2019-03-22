using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MercadoApi.Models;

namespace MercadoApi.Controllers
{
    [Route("mercado/funcionarios")]
    [ApiController]
    public class MercadoController : ControllerBase
    {
        private readonly Context _context;

        public MercadoController(Context context)
        {
            _context = context;
            
            if (_context.Funcionarios.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.Funcionarios.Add(new Funcionario { Nome = "Victor", Cpf = "12345",Sexo = "M", Turno = "Tarde", Salario =  10.50});
                _context.SaveChanges();
            }
        }

    // GET: 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Funcionario>>> GetFuncionarios()
    {
        return await _context.Funcionarios.ToListAsync();
    }

    // GET with id: 
    [HttpGet("{id}")]
    public async Task<ActionResult<Funcionario>> GetFuncionarios(int id)
    {
        var funcionario = await _context.Funcionarios.FindAsync(id);

        if (funcionario == null)
        {
            return NotFound();
        }

        return funcionario;
    }

    // POST: 
    [HttpPost]
    public async Task<ActionResult<Funcionario>> PostFuncionarios(Funcionario item)
    {
        _context.Funcionarios.Add(item);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetFuncionarios), new { id = item.Id }, item);
    }

    // PUT: 
    [HttpPut("{id}")]
    public async Task<IActionResult> PutFuncionarios(long id, Funcionario item)
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
        var funcionario = await _context.Funcionarios.FindAsync(id);

        if (funcionario == null)
        {
            return NotFound();
        }

        _context.Funcionarios.Remove(funcionario);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    }
}