using Microsoft.EntityFrameworkCore;
namespace LP2_FINAL
{
    public class Context: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)         
        {             
            optionsBuilder.UseSqlite("Data Source=SuperMercadoDataBase.db");         
        }
    }
}