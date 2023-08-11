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
            var todos = dbContext.Todos.Include(todo => todo.Category_Todos).ThenInclude(todo => todo.Category).AsQueryable();

            // Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                //Filter On Name
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    todos = todos.Where(todo => todo.Name.Contains(filterQuery));
                }
                // Filter On Category Name
                else if (filterOn.Equals("Category", StringComparison.OrdinalIgnoreCase))
                {
                    todos = todos.Where(todo => todo.Category_Todos.Any(ct => ct.Category.Name.Contains(filterQuery)));
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
            }

            //Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            // Skip -> Results to skip - Take -> Number of results to retrieve 
            return await todos.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Todo?> GetByIdAsync(Guid id)
        {
            return await dbContext.Todos
                .Include(todo => todo.Category_Todos).ThenInclude(todo => todo.Category)
                .FirstOrDefaultAsync(todo => todo.Id == id);
        }

        public async Task<Todo> CreateAsync(Todo todo, List<Guid> categoryIds)
        {
            // Add Todo
            await dbContext.Todos.AddAsync(todo);

            var todoId = todo.Id;

            // Create relation between Todo and Cat
            // Add entries to relational table
            // One for each category
            var Category_TodoList = new List<Category_Todo>();

            foreach (Guid categoryId in categoryIds)
            {
                Category_TodoList.Add(new Category_Todo()
                {
                    TodoId = todoId,
                    CategoryId = categoryId
                });
            }

            await dbContext.Category_Todos.AddRangeAsync(Category_TodoList);

            await dbContext.SaveChangesAsync();

            // Return Created TODO
            // Added this logic to return Todo with Categories associated
            var createdTodo = await dbContext.Todos
                .Include(todo => todo.Category_Todos).ThenInclude(todo => todo.Category)
                .FirstOrDefaultAsync(todo => todo.Id == todoId);

            return createdTodo != null ? createdTodo : todo;
        }

        public async Task<Todo?> UpdateAsync(Guid id, Todo todo, List<Guid> categoryIds)
        {
            var existingTodo = await dbContext.Todos.FirstOrDefaultAsync(todo => todo.Id == id);

            if (existingTodo == null)
            {
                return null;
            }

            existingTodo.Name = todo.Name;
            existingTodo.Description = todo.Description;
            existingTodo.Done = todo.Done;

            // Updating Categories on TODOS

            // Remove entries with this todoId from Category_Todos
            var categoriesToDelete = await dbContext.Category_Todos.Where(ct => ct.TodoId == id).ToListAsync();
            dbContext.Category_Todos.RemoveRange(categoriesToDelete);
            await dbContext.SaveChangesAsync();

            // Add new entries with the new categories
            var Category_TodoList = new List<Category_Todo>();

            foreach (Guid categoryId in categoryIds)
            {
                Category_TodoList.Add(new Category_Todo()
                {
                    TodoId = id,
                    CategoryId = categoryId
                });
            }

            await dbContext.Category_Todos.AddRangeAsync(Category_TodoList);
            await dbContext.SaveChangesAsync();

            // Return Updated TODO
            // Added this logic to return Todo with Categories associated

            var updatedTodo = await dbContext.Todos
                .Include(todo => todo.Category_Todos).ThenInclude(todo => todo.Category)
                .FirstOrDefaultAsync(todo => todo.Id == id);

            return updatedTodo != null ? updatedTodo : existingTodo;
        }

        public async Task<Todo?> DeleteAsync(Guid id)
        {
            var existingTodo = await dbContext.Todos
                .Include(todo => todo.Category_Todos).ThenInclude(todo => todo.Category)
                .FirstOrDefaultAsync(todo => todo.Id == id);

            if (existingTodo == null)
            {
                return null;
            }

            dbContext.Todos.Remove(existingTodo);
            await dbContext.SaveChangesAsync();

            return existingTodo;
        }
    }
}
