using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MercadoApi.Models;

namespace MercadoApi.Controllers
{
    [Route("mercado/estoques")]
    [ApiController]
    public class EstoqueController : ControllerBase
    {
        private readonly Context _context;

        public EstoqueController(Context context)
        {
            _context = context;
        }

    // GET: 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Estoque>>> GetEstoques()
    {
        return await _context.Estoques.ToListAsync();
    }

    // GET produtos of estoque ID: 
    [HttpGet("{id}/produtos")]
    public async Task<ActionResult<Object>> GetEstoqueProduto(int id)
    {
        var listProdutos = _context.Produtos
            .Where(o => o.EstoqueId == id) 
            .Distinct() 
            .ToList();
        var estoque = await _context.Estoques.FindAsync(id);
        if (estoque == null || listProdutos == null)
        {
            return NotFound();
        }
        Object[] newListProdutos = new Object[listProdutos.Count];
        int i = 0;
        foreach(var produto in listProdutos){
            var json = new {
                Codigo = produto.Codigo,
                Nome = produto.Nome,
                Preco = produto.Preco,
                Quantidade = produto.Quantidade,
                Peso = produto.Peso,
                Desconto = produto.Desconto,
                Categoria = produto.Categoria,
                Validade = produto.Validade,
                EstoqueId = estoque.Id
            };
            newListProdutos[i] = json;
            i++;
        }
        return newListProdutos.ToList();
    }

    // POST: 
    [HttpPost]
    public async Task<ActionResult<Estoque>> PostEstoques(Estoque item)
    {
        _context.Estoques.Add(item);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEstoques), new { id = item.Id }, item);
    }

    // PUT: 
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEstoque(int id, Estoque item)
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
    public async Task<IActionResult> DeleteEstoque(int id)
    {
        var estoque = await _context.Estoques.FindAsync(id);

        if (estoque == null)
        {
            return NotFound();
        }
        var listProdutos = _context.Produtos
            .Where(o => o.EstoqueId == id) 
            .Distinct() 
            .ToList();
        foreach(var produto in listProdutos){
            var json = await _context.Produtos.FindAsync(produto.Id);
            json.EstoqueId = null;
            _context.Produtos.Update(json);
            await _context.SaveChangesAsync();
            _context.Entry(json).State = EntityState.Detached;
        }
        _context.Estoques.Remove(estoque);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    
    }
}