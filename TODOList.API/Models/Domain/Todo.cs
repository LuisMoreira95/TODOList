using System.Reflection.Metadata.Ecma335;

namespace TODOList.API.Models.Domain
{
    public class Todo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        
        // Navigation properties - Definem as relações do Domain
        // Quando corrermos o Entity Framework Migrations
        public Category Category { get; set; }
    }
}
