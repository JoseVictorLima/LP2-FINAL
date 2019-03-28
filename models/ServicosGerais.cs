using System.ComponentModel.DataAnnotations.Schema;

namespace MercadoApi.Models
{
    public class ServicosGerais
    {
        public int Id{get;set;}
        [ForeignKey("Funcionario")]
        public int FuncionarioId{get;set;}
        public Funcionario Funcionario{get;set;}
    }
}