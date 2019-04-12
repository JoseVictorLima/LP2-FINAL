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
            
            if (_context.ServicosGerais.Count() == 0)
            {
                _context.ServicosGerais.Add(new ServicosGerais {Funcionario = new Funcionario{Nome = "ServicosGerais", Cpf = "3",Sexo = "M", Turno = "Tarde", Salario =  1500.50}});
                _context.SaveChanges();
            }
        }

    // GET: 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Object>>> GetServicosGerais()
    {
        var servicoGeralList = await _context.ServicosGerais.ToListAsync();
        Object[] newServicoGeralList = new Object[servicoGeralList.Count];
        int i = 0;
        foreach(var servicoGeral in servicoGeralList){
            var funcionario = await _context.Funcionarios.FindAsync(servicoGeral.FuncionarioId);
            var json = new{
                id = servicoGeral.Id,
                funcionario =  funcionario
            };
            System.Diagnostics.Debug.WriteLine(json);
            newServicoGeralList[i] = json;
            i++;
        }
       
        return  newServicoGeralList.ToList();
    }

    // GET with id: 
    [HttpGet("{id}")]
    public async Task<ActionResult<Object>> GetServicosGerais(int id)
    {
        var servicoGeral = await _context.ServicosGerais.FindAsync(id);
        var funcionario = await _context.Funcionarios.FindAsync(servicoGeral.FuncionarioId);
        if (servicoGeral == null)
        {
            return NotFound();
        }
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
        var servicoGeral = await _context.ServicosGerais.FindAsync(id);

        if (servicoGeral == null)
        {
            return NotFound();
        }

        _context.ServicosGerais.Remove(servicoGeral);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    }
}