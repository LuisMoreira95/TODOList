using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TODOList.API.Data;
using TODOList.API.Models.Domain;
using TODOList.API.Models.DTO;
using TODOList.API.Repositories;

namespace TODOList.API.Controllers
{
    // https://localhost:7082/api/todos
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly TodoListDbContext dbContext;
        private readonly ITodoRepository todoRepository;

        public TodosController(TodoListDbContext dbContext, ITodoRepository todoRepository)
        {
            this.dbContext = dbContext;
            this.todoRepository = todoRepository;
        }

        // GET ALL TODOS
        // Get: https://localhost:7082/api/todos
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {   
            // Get Data From Database - Domain Models
            var todosDomain = await todoRepository.GetAllAsync();

            //Map Domain Model to Dto
            var todosDto = new List<TodoDto>();
            foreach (var todoDomain in todosDomain) 
            {
                todosDto.Add(new TodoDto()
                {
                    Id = todoDomain.Id,
                    Name = todoDomain.Name,
                    Description = todoDomain.Description,
                    CategoryName = todoDomain.CategoryName,
                });                    
                
            }
            // Return DTO
            return Ok(todosDto);
        }

        // GET SPECIFIC TODO (Get Todo By ID)
        // Get: https://localhost:7082/api/todos/{id}
        [HttpGet]
        [Route("id:Guid")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // Get Data From Database - Domain Models
            var todoDomain = await todoRepository.GetByIdAsync(id);

            if (todoDomain is null)
            {
                return NotFound();
            }
            
            //Map Domain Model to Dto
            var todoDto = new TodoDto
            {
                Id = todoDomain.Id,
                Name = todoDomain.Name,
                Description = todoDomain.Description,
                CategoryName = todoDomain.CategoryName,
            };
            
            // Return DTO
            return Ok(todoDto);
        }

        // POST To Create New TODO
        // POST: https://localhost:7082/api/todos/
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddTodoRequestDto addTodoRequestDto)
        {
            // Mapo Dto to Domain Model
            var todoDomainModel = new Todo
            {
                Name = addTodoRequestDto.Name,
                Description = addTodoRequestDto?.Description,
                CategoryName = addTodoRequestDto.CategoryName,
            };

            // Use Domain Model to Create Todo
            todoDomainModel = await todoRepository.CreateAsync(todoDomainModel);

            // Map Domain to DTO
            var todoDto = new TodoDto
            {
                Id = todoDomainModel.Id,
                Name = todoDomainModel.Name,
                Description = todoDomainModel?.Description,
                CategoryName = todoDomainModel.CategoryName,
            };

            return CreatedAtAction(nameof(GetById), new { id = todoDto.Id }, todoDto);
        }

        // PUT To Update TODO
        // PUT: https://localhost:7082/api/todos/{id}
        [HttpPut]
        [Route("id:Guid")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateTodoRequestDto updateTodoRequestDto )
        {
            // Map DTO To Domain Model
            var todoDomainModel = new Todo
            {
                Name = updateTodoRequestDto.Name,
                Description = updateTodoRequestDto.Description,
                CategoryId = updateTodoRequestDto.CategoryId,
                CategoryName = updateTodoRequestDto.CategoryName,
            };

            // Updates Todo or Returns null
            todoDomainModel = await todoRepository.UpdateAsync(id, todoDomainModel);

            if ( todoDomainModel == null )
            {
                return NotFound();
            }

            // Domain Model to DTO
            var todoDto = new TodoDto
            {
                Name = todoDomainModel.Name,
                Description = todoDomainModel.Description,
                // CategoryId = todoDomainModel.CategoryId,
                CategoryName = todoDomainModel.CategoryName,
            };

            return Ok(todoDto);
        }

        // DELETE To Delete TODO
        // DELETE: https://localhost:7082/api/todos/{id}
        [HttpDelete]
        [Route("id:Guid")]
        public async Task<IActionResult> Delete([FromRoute] Guid id) 
        {
            // Deletes TODO or Returns null
            var todoDomainModel = await todoRepository.DeleteAsync(id);
            
            if ( todoDomainModel == null )
            {
                return NotFound();
            }

            // Map Domain Model to DTO
            var todoDto = new TodoDto
            {
                Name = todoDomainModel.Name,
                Description = todoDomainModel.Description,
                // CategoryId = todoDomainModel.CategoryId,
                CategoryName = todoDomainModel.CategoryName,
            };

            return Ok(todoDto);
        }

    }
}
