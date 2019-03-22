using Microsoft.EntityFrameworkCore;
namespace MercadoApi.Models
{
    public class Context: DbContext
    {
        public Context(DbContextOptions<Context> options)         
            :base(options)         
        {   
        }
        public DbSet<Funcionario> Funcionarios {get; set;}  
        public DbSet<Gerente> Gerentes {get; set;}   
        public DbSet<Operador> Operadores {get; set;}
        public DbSet<ServicosGerais> ServicosGerais {get; set;}
        public DbSet<Estoque> Estoques {get; set;}
        public DbSet<Caixa> Caixas {get; set;}
        public DbSet<Relatorio> Relatorios {get; set;}
        public DbSet<Produto> Produtos {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)         
        {             optionsBuilder.UseSqlite("Data Source=SuperMercadoDataBase.db");         
        }    
        
    }
}