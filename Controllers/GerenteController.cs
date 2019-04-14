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
        }

    // GET: 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Gerente>>> GetGerentes()
    {
        var gerenteList = await _context.Gerentes.Include(gerente => gerente.Funcionario).ToListAsync();
       
        return  gerenteList;
    }

    // GET with id: 
    [HttpGet("{id}")]
    public async Task<ActionResult<Object>> GetGerente(int id)
    {
        var gerente = await _context.Gerentes.FindAsync(id);
        if (gerente == null)
        {
            return NotFound();
        }
        var funcionario = await _context.Funcionarios.FindAsync(gerente.FuncionarioId);
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
    public async Task<IActionResult> PutGerente(int id, Gerente item)
    {
        if (id != item.Id)
        {
            return BadRequest();
        }
        _context.Funcionarios.Update(item.Funcionario);
        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }


    // DELETE with id:
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGerente(int id)
    {
        var gerente = await _context.Gerentes.FindAsync(id);
        if (gerente == null)
        {
            return NotFound();
        }
        var funcionario = await _context.Funcionarios.FindAsync(gerente.FuncionarioId);
        if (funcionario == null)
        {
            return NotFound();
        }
        _context.Funcionarios.Remove(funcionario);
        _context.Gerentes.Remove(gerente);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    }
}