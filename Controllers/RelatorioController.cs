using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MercadoApi.Models;

namespace MercadoApi.Controllers
{
    [Route("mercado/relatorios")]
    [ApiController]
    public class RelatorioController : ControllerBase
    {
        private readonly Context _context;

        public RelatorioController(Context context)
        {
            _context = context;
            
            if (_context.Relatorios.Count() == 0)
            {
                _context.Relatorios.Add(new Relatorio{Dado="laslas",Funcionario = new Funcionario {Nome = "ServicosGerais", Cpf = "3",Sexo = "M", Turno = "Tarde", Salario =  1500.50}});
                _context.SaveChanges();
            }
        }

    // GET: 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Object>>> GetServicosGerais()
    {
        var relatoriosList = await _context.Relatorios.ToListAsync();
        Object[] newRelatoriosList = new Object[relatoriosList.Count];
        int i = 0;
        foreach(var relatorio in relatoriosList){
           var funcionario = await _context.Relatorios.FindAsync(relatorio.FuncionarioId);
            var json = new{
                id = relatorio.Id,
                funcionario =  funcionario
            };
            System.Diagnostics.Debug.WriteLine(json);
            newRelatoriosList[i] = json;
            i++;
        }
       
        return  newRelatoriosList.ToList();
    }

    // GET with id: 
    [HttpGet("{id}")]
    public async Task<ActionResult<Object>> GetRelatorios(int id)
    {
        var relatorio = await _context.Relatorios.FindAsync(id);
        var funcionario = await _context.Funcionarios.FindAsync(relatorio.FuncionarioId);
        if (relatorio == null)
        {
            return NotFound();
        }
        var relatorioFormatado = new {
            id = relatorio.Id,
            funcionario = funcionario
        };
        return relatorioFormatado;
    }

    // POST: 
    [HttpPost]
    public async Task<ActionResult<Relatorio>> PostRelatorios(Relatorio item)
    {
        _context.Relatorios.Add(item);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetRelatorios), new { id = item.Id }, item);
    }

    // PUT: 
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRelatorios(long id, Relatorio item)
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
        var relatorio = await _context.Relatorios.FindAsync(id);

        if (relatorio == null)
        {
            return NotFound();
        }

        _context.Relatorios.Remove(relatorio);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    }
}