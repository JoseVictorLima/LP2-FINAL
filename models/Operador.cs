namespace MercadoApi.Models
{

    public class Operador
    {
        public int Id{get;set;}
        public virtual Funcionario Funcionario{get;set;}
    }
}