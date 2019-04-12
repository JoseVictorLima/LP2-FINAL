using System.ComponentModel.DataAnnotations.Schema;
namespace MercadoApi.Models
{
    public class Relatorio
    {
        public int Id{get;set;}
        public string Dado{get;set;}
        [ForeignKey("Funcionario")]
        public int FuncionarioId{get;set;}
        public Funcionario Funcionario{get;set;}
    }
}