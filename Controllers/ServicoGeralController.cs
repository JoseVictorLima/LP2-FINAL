using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MercadoApi.Models;

namespace MercadoApi.Controllers
{
    [Route("mercado/servicos-gerais")]
    [ApiController]
    public class ServicoGeralController : ControllerBase
    {
        private readonly Context _context;

        public ServicoGeralController(Context context)
        {
            _context = context;
            
            if (_context.ServicosGerais.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.ServicosGerais.Add(new ServicosGerais {Funcionario = new Funcionario{Nome = "ServicosGerais", Cpf = "3",Sexo = "M", Turno = "Tarde", Salario =  1500.50}});
                _context.SaveChanges();
            }
        }

    // GET: 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServicosGerais>>> GetServicosGerais()
    {
        return await _context.ServicosGerais.ToListAsync();
    }

    // GET with id: 
    [HttpGet("{id}")]
    public async Task<ActionResult<ServicosGerais>> GetServicosGerais(int id)
    {
        var gerente = await _context.ServicosGerais.FindAsync(id);

        if (gerente == null)
        {
            return NotFound();
        }

        return gerente;
    }

    // POST: 
    [HttpPost]
    public async Task<ActionResult<ServicosGerais>> PostServicosGerais(ServicosGerais item)
    {
        _context.ServicosGerais.Add(item);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetServicosGerais), new { id = item.Id }, item);
    }

    // PUT: 
    [HttpPut("{id}")]
    public async Task<IActionResult> PutServicosGerais(long id, ServicosGerais item)
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
        var gerente = await _context.ServicosGerais.FindAsync(id);

        if (gerente == null)
        {
            return NotFound();
        }

        _context.ServicosGerais.Remove(gerente);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    }
}