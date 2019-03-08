using Microsoft.EntityFrameworkCore;
namespace LP2_FINAL.models
{
    public class Context: DbContext
    {
        public DbSet<Funcionario> Funcionarios {get; set;}  
        public DbSet<Gerente> Gerentes {get; set;}   
        public DbSet<Operador> Operadores {get; set;}
        public DbSet<ServicosGerais> ServicosGerais {get; set;}
        public DbSet<Estoque> Estoques {get; set;}
        public DbSet<Caixa> Caixas {get; set;}
        public DbSet<Relatorio> Relatorios {get; set;}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)         
        {   
            optionsBuilder.UseSqlite("Data Source=SuperMercadoDataBase.db");         
        }
    }
}