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
    public async Task<ActionResult<IEnumerable<Object>>> GetOperadores()
    {
        var operadorList = await _context.Operadores.ToListAsync();
        Object[] newOperadorList = new Object[operadorList.Count];
        int i = 0;
        foreach(var operador in operadorList){
            var funcionario = await _context.Funcionarios.FindAsync(operador.FuncionarioId);
            var json = new{
                id = operador.Id,
                funcionario =  funcionario
            };
            System.Diagnostics.Debug.WriteLine(json);
            newOperadorList[i] = json;
            i++;
        }
       
        return  newOperadorList.ToList();
    }

    // GET with id: 
    [HttpGet("{id}")]
    public async Task<ActionResult<Object>> GetOperadores(int id)
    {
        var operador = await _context.Operadores.FindAsync(id);
        var funcionario = await _context.Funcionarios.FindAsync(operador.FuncionarioId);
        if (operador == null)
        {
            return NotFound();
        }
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
        var operador = await _context.Operadores.FindAsync(id);

        if (operador == null)
        {
            return NotFound();
        }

        _context.Operadores.Remove(operador);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    }
}