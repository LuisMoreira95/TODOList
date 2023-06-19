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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Data for Categories
            // Work, Pet, Personal, Finance, Other
            var categories = new List<Category>()
            {
                new Category()
                {
                Id = Guid.Parse("6b55a90f-ea79-4e85-accf-540be8f2e2a0") ,
                Name = "Work",
                },
                new Category()
                {
                Id = Guid.Parse("12ccd14e-21af-4801-bf14-e80d6b1a2ff5") ,
                Name = "Pet",
                },
                new Category()
                {
                Id = Guid.Parse("4521d9f7-c05f-4f76-aa18-6fba6b70e1bc"),
                Name = "Personal",
                },
                new Category()
                {
                Id = Guid.Parse("1ee26f62-463f-4125-801f-7fe91ae329ba"),
                Name = "Finance",
                },
                new Category()
                {
                Id = Guid.Parse("a65d0695-4f8b-49a7-a09a-5e0ec06a142e") ,
                Name = "Other",
                }
            };

            // Seed Categories to the Database
            modelBuilder.Entity<Category>().HasData(categories);
        }

    }
}
