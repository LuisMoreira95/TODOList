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

        public async Task<List<Todo>> GetAllAsync()
        {
            return await dbContext.Todos.Include("Category").ToListAsync();
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
