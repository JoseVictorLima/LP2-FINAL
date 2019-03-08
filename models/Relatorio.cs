namespace MercadoApi.Models
{
    public class Relatorio
    {
        public int Id{get;set;}
        public string Dado{get;set;}
        public virtual Funcionario Funcionario{get;set;}
    }
}