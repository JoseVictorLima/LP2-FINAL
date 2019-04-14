using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MercadoApi.Models;

namespace MercadoApi.Controllers
{
    [Route("mercado/produtos")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly Context _context;

        public ProdutoController(Context context)
        {
            _context = context;
        }

    // GET: 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
    {
        return await _context.Produtos.ToListAsync();
    }

    // GET with id: 
    [HttpGet("{id}")]
    public async Task<ActionResult<Produto>> GetProduto(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);

        if (produto == null)
        {
            return NotFound();
        }

        return produto;
    }

    // POST: 
    [HttpPost]
    public async Task<ActionResult<Produto>> PostProdutos(Produto item)
    {
        if(item.Estoque == null){

        _context.Produtos.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProdutos), new { id = item.Id }, item);

        }else{

            var estoque = await _context.Estoques.FindAsync(item.Estoque.Id);

            if(estoque == null){
                return BadRequest();
            }
            var produto = new Produto(){
                Codigo = item.Codigo,
                Nome = item.Nome,
                Preco = item.Preco,
                Quantidade = item.Quantidade,
                Peso = item.Peso,
                Desconto = item.Desconto,
                Categoria = item.Categoria,
                Validade = item.Validade,
                EstoqueId = estoque.Id
             };
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProdutos), new { id = produto.Id }, produto);
        }
    }

    // PUT: 
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduto(int id, Produto item)
    {
        if (id != item.Id)
        {
            return BadRequest();
        }

        if(item.Estoque != null){
            var estoque = await _context.Estoques.FindAsync(item.Estoque.Id);
            item.EstoqueId = estoque.Id;
        }
        else{
            var produto = await _context.Produtos.FindAsync(item.Id);
            item.EstoqueId = produto.EstoqueId;
            _context.Entry(produto).State = EntityState.Detached;
        }
        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE with id:
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduto(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);

        if (produto == null)
        {
            return NotFound();
        }

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    }
}