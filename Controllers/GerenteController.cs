using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MercadoApi.Models;

namespace MercadoApi.Controllers
{
    [Route("mercado/gerentes")]
    [ApiController]
    public class GerenteController : ControllerBase
    {
        private readonly Context _context;

        public GerenteController(Context context)
        {
            _context = context;
            
            if (_context.Gerentes.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                var gerente = new Gerente {Funcionario = new Funcionario{Nome = "Gerente", Cpf = "1",Sexo = "M", Turno = "Manhã,Tarde,Noite", Salario =  5000.00}};
                _context.Gerentes.Add(gerente);
                _context.SaveChanges();
            }
        }

    //SSL
    // GET: 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Object>>> GetGerentes()
    {
        var gerenteList = await _context.Gerentes.ToListAsync();
        Object[] newGerenteList = new Object[gerenteList.Count];
        int i = 0;
        foreach(var gerente in gerenteList){
            var funcionario = await _context.Funcionarios.FindAsync(gerente.FuncionarioId);
            var json = new{
                id = gerente.Id,
                funcionario =  funcionario
            };
            System.Diagnostics.Debug.WriteLine(json);
            newGerenteList[i] = json;
            i++;
        }
       
        return  newGerenteList.ToList();
    }

    // GET with id: 
    [HttpGet("{id}")]
    public async Task<ActionResult<Object>> GetGerentes(int id)
    {
        var gerente = await _context.Gerentes.FindAsync(id);
        var funcionario = await _context.Funcionarios.FindAsync(gerente.FuncionarioId);
        if (gerente == null)
        {
            return NotFound();
        }
        var gerenteFormatado = new {
            id = gerente.Id,
            funcionario = funcionario
        };
        return gerenteFormatado;
    }

    // POST: 
    [HttpPost]
    public async Task<ActionResult<Gerente>> PostGerentes(Gerente item)
    {
        _context.Gerentes.Add(item);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetGerentes), new { id = item.Id }, item);
    }

    // PUT: 
    [HttpPut("{id}")]
    public async Task<IActionResult> PutGerentes(long id, Gerente item)
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
        var gerente = await _context.Gerentes.FindAsync(id);

        if (gerente == null)
        {
            return NotFound();
        }

        _context.Gerentes.Remove(gerente);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    }
}