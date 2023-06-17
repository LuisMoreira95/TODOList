using Microsoft.EntityFrameworkCore;
using TODOList.API.Models.Domain;

namespace TODOList.API.Data
{
    public class TodoListDbContext : DbContext
    {
        public TodoListDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        // Propriedades que criam as tabelas em SQL
        public DbSet<Todo> Todos { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
