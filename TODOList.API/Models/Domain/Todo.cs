using System.Reflection.Metadata.Ecma335;

namespace TODOList.API.Models.Domain
{
    public class Todo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool Done { get; set; }

        // Navigation properties - Define relations on domain
        // When Entity Framework Migrations is executed
        public List<Category_Todo> Category_Todos { get; set; }
    }
}
