using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MercadoApi.Models;

namespace MercadoApi.Controllers
{
    [Route("api/Funcionarios")]
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

    // GET: api/Funcionario
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Funcionario>>> GetTodoItems()
    {
        return await _context.Funcionarios.ToListAsync();
    }

    // GET: api/Todo/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Funcionario>> GetTodoItem(long id)
    {
        var funcionario = await _context.Funcionarios.FindAsync(id);

        if (funcionario == null)
        {
            return NotFound();
        }

        return funcionario;
    }
    }
}