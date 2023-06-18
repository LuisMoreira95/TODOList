using TODOList.API.Models.Domain;

namespace TODOList.API.Repositories
{
    public interface ITodoRepository
    {
        Task<List<Todo>> GetAllAsync();
        Task<Todo?> GetByIdAsync(Guid id);
        Task<Todo> CreateAsync(Todo todo);
        Task<Todo?> UpdateAsync(Guid id, Todo todo);
        Task<Todo?> DeleteAsync(Guid id);

    }
}
