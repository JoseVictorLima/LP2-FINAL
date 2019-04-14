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
        }

    // GET: 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Object>>> GetServicosGerais()
    {
        var relatoriosList = await _context.Relatorios.ToListAsync();
        Object[] newRelatoriosList = new Object[relatoriosList.Count];
        int i = 0;
        foreach(var relatorio in relatoriosList)
        {
           var funcionario = await _context.Funcionarios.FindAsync(relatorio.FuncionarioId);
            var json = new{
                id = relatorio.Id,
                dado = relatorio.Dado,
                funcionario =  funcionario
            };
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
            dado = relatorio.Dado,
            funcionario = funcionario
        };
        return relatorioFormatado;
    }

    // POST: 
    [HttpPost]
    public async Task<ActionResult<Relatorio>> PostRelatorios(Relatorio item)
    {
        if(item.Funcionario !=null)
        {
            var funcionario = await _context.Funcionarios.FindAsync(item.Funcionario.Id);
            if(funcionario == null)
            {
                return BadRequest();
            }
            item.Funcionario = funcionario;
            _context.Entry(funcionario).State = EntityState.Detached;
            _context.Funcionarios.Update(item.Funcionario);
        }

        _context.Relatorios.Add(item);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetRelatorios), new { id = item.Id }, item);
    }

    // PUT: 
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRelatorios(int id, Relatorio item)
    {
        var relatorio = await _context.Relatorios.FindAsync(id);
        if (id != item.Id)
        {
            return BadRequest();
        }

        var funcionario = await _context.Funcionarios.FindAsync(relatorio.FuncionarioId);
        if(funcionario == null)
        {
            return BadRequest();
        }

        item.Funcionario = funcionario;
        _context.Entry(relatorio).State = EntityState.Detached;
        _context.Entry(funcionario).State = EntityState.Detached;
        _context.Funcionarios.Update(item.Funcionario);
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