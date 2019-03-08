using System.Collections.Generic;

namespace MercadoApi.Models
{
    public class Estoque
    {
        public int Id{get;set;}
        public Estoque(){
            Produtos = new List<Produto>();
        }

        public virtual ICollection<Produto> Produtos{get;set;}
    }
}