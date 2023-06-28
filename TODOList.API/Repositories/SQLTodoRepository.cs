using Microsoft.EntityFrameworkCore;
using TODOList.API.Data;
using TODOList.API.Models.Domain;

namespace TODOList.API.Repositories
{
    public class SQLTodoRepository : ITodoRepository
    {
        private readonly TodoListDbContext dbContext;

        public SQLTodoRepository(TodoListDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Todo>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 1000)
        {
            var todos = dbContext.Todos.Include("Category").AsQueryable();

            // Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                //Filter On Name
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    todos = todos.Where(todo => todo.Name.Contains(filterQuery));
                }
                //Filter On Category Name
                else if (filterOn.Equals("Category", StringComparison.OrdinalIgnoreCase))
                {
                    todos = todos.Where(todo => todo.Category.Name.Contains(filterQuery));
                }
            }

            // Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                // Sort On Name
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    todos = isAscending ? todos.OrderBy(todo => todo.Name) : todos.OrderByDescending(todo => todo.Name);
                }
                //Sort On Category Name
                else if (sortBy.Equals("Category", StringComparison.OrdinalIgnoreCase)) 
                {
                    todos = isAscending ? todos.OrderBy(todo => todo.Category.Name) : todos.OrderByDescending(todo => todo.Category.Name);
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            // Skip -> Results to skip - Take -> Number of results to retrieve 
            return await todos.Skip(skipResults).Take(pageSize).ToListAsync(); 
        }

        public async Task<Todo?> GetByIdAsync(Guid id)
        {
            return await dbContext.Todos
                .Include("Category")
                .FirstOrDefaultAsync(todo => todo.Id == id);
        }
        
        public async Task<Todo> CreateAsync(Todo todo)
        {
            await dbContext.Todos.AddAsync(todo);
            await dbContext.SaveChangesAsync();
            return todo;
        }
        
        public async Task<Todo?> UpdateAsync(Guid id, Todo todo)
        {
            var existingTodo = await dbContext.Todos.FirstOrDefaultAsync(todo => todo.Id == id);
            
            if (existingTodo == null) 
            {
                return null;
            }

            existingTodo.Name = todo.Name;
            existingTodo.Description = todo.Description;
            existingTodo.CategoryId = todo.CategoryId;

            await dbContext.SaveChangesAsync();
            
            return existingTodo;
        }

        public async Task<Todo?> DeleteAsync(Guid id)
        {
            var existingTodo = await dbContext.Todos.FirstOrDefaultAsync(todo => todo.Id == id);

            if(existingTodo == null)
            {
                return null;
            }

            dbContext.Todos.Remove(existingTodo);
            await dbContext.SaveChangesAsync();

            return existingTodo;
        }
    }
}
