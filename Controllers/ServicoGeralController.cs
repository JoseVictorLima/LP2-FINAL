using System;
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
        }

    // GET: 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Object>>> GetServicosGerais()
    {
        var servicoGeralList = await _context.ServicosGerais.Include(servicoGeral => servicoGeral.Funcionario).ToListAsync();
       
        return  servicoGeralList;
    }

    // GET with id: 
    [HttpGet("{id}")]
    public async Task<ActionResult<Object>> GetServicoGeral(int id)
    {
        var servicoGeral = await _context.ServicosGerais.FindAsync(id);
        if (servicoGeral == null)
        {
            return NotFound();
        }
        var funcionario = await _context.Funcionarios.FindAsync(servicoGeral.FuncionarioId);
        var servicoGeralFormatado = new {
            id = servicoGeral.Id,
            funcionario = funcionario
        };
        return servicoGeralFormatado;
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
    public async Task<IActionResult> PutServicoGeral(int id, ServicosGerais item)
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
    public async Task<IActionResult> DeleteTodoItem(int id)
    {
        var servicoGeral = await _context.ServicosGerais.FindAsync(id);

        if (servicoGeral == null)
        {
            return NotFound();
        }
        var funcionario = await _context.Funcionarios.FindAsync(servicoGeral.FuncionarioId);
        if (funcionario == null)
        {
            return NotFound();
        }
        _context.Funcionarios.Remove(funcionario);
        _context.ServicosGerais.Remove(servicoGeral);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    }
}