using TODOList.API.Models.Domain;

namespace TODOList.API.Repositories
{
    // Repositories Always Deal With Domain Models
    public interface ITodoRepository
    {
        Task<List<Todo>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 1000);
        Task<Todo?> GetByIdAsync(Guid id);
        Task<Todo> CreateAsync(Todo todo);
        Task<Todo?> UpdateAsync(Guid id, Todo todo);
        Task<Todo?> DeleteAsync(Guid id);

    }
}
