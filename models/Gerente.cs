namespace MercadoApi.Models
{
    public class Gerente
    {
        public int Id{get;set;}
        public virtual Funcionario Funcionario{get;set;}
    }
}