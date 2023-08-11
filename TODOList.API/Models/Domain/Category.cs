namespace TODOList.API.Models.Domain
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        // Navigation properties - Define relations on domain
        // When Entity Framework Migrations is executeds
        public List<Category_Todo> Category_Todos { get; set; }
    }
}