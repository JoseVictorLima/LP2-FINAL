namespace MercadoApi.Models
{
    public class Produto
    {
        public int Id{get;set;}
        public string Codigo{get;set;}
        public string Nome{get;set;}
        public double Preco{get;set;}
        public int Quantidade{get;set;}
        public double Peso{get;set;}
        public int Desconto{get;set;}
        public string Categoria{get;set;}
        public int Validade{get;set;}

    }
}