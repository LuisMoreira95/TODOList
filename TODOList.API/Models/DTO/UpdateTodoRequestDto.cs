namespace TODOList.API.Models.DTO
{
    public class UpdateTodoRequestDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid CategoryId { get; set; }
    }
}
