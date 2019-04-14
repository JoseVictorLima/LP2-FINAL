using System;
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
        }

    // GET: 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Object>>> GetOperadores()
    {
        var operadorList = await _context.Operadores.Include(operador => operador.Funcionario).ToListAsync();
       
        return  operadorList;
    }

    // GET with id: 
    [HttpGet("{id}")]
    public async Task<ActionResult<Object>> GetOperador(int id)
    {
        var operador = await _context.Operadores.FindAsync(id);
        if (operador == null)
        {
            return NotFound();
        }
        var funcionario = await _context.Funcionarios.FindAsync(operador.FuncionarioId);
        var operadorFormatado = new {
            id = operador.Id,
            funcionario = funcionario
        };
        return operadorFormatado;
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
    public async Task<IActionResult> PutOperador(int id, Operador item)
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
    public async Task<IActionResult> DeleteOperador(int id)
    {
        var operador = await _context.Operadores.FindAsync(id);

        if (operador == null)
        {
            return NotFound();
        }
        var funcionario = await _context.Funcionarios.FindAsync(operador.FuncionarioId);
        if (funcionario == null)
        {
            return NotFound();
        }
        _context.Funcionarios.Remove(funcionario);
        _context.Operadores.Remove(operador);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    }
}