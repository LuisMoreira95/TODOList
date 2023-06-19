namespace TODOList.API.Models.DTO
{
    public class AddTodoRequestDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid CategoryId { get; set; }
    }
}
