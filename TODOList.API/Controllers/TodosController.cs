using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TODOList.API.Data;
using TODOList.API.Models.Domain;
using TODOList.API.Models.DTO;

namespace TODOList.API.Controllers
{
    // https://localhost:7082/api/todos
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly TodoListDbContext dbContext;

        public TodosController(TodoListDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET ALL TODOS
        // Get: https://localhost:7082/api/todos
        [HttpGet]
        public IActionResult GetAll()
        {   
            // Get Data From Database - Domain Models
            var todosDomain = dbContext.Todos.ToList();

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
        public IActionResult GetById([FromRoute] Guid id)
        {
            // Get Data From Database - Domain Models
            var todoDomain = dbContext.Todos.Find(id);

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
        public IActionResult Create([FromBody] AddTodoRequestDto addTodoRequestDto)
        {
            // Mapo Dto to Domain Model
            var todoDomainModel = new Todo
            {
                Name = addTodoRequestDto.Name,
                Description = addTodoRequestDto?.Description,
                CategoryName = addTodoRequestDto.CategoryName,
            };

            // Use Domain Model to Create Todo
            dbContext.Todos.Add(todoDomainModel);
            dbContext.SaveChanges();

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
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateTodoRequestDto updateTodoRequestDto )
        {
            // Check if todo exists
            var todoDomainModel = dbContext.Todos.FirstOrDefault(todo => todo.Id == id);

            if ( todoDomainModel == null )
            {
                return NotFound();
            }
            
            // Map DTO To Domain Model
            todoDomainModel.Name = updateTodoRequestDto.Name;
            todoDomainModel.Description = updateTodoRequestDto.Description;
            todoDomainModel.CategoryId = updateTodoRequestDto.CategoryId;
            todoDomainModel.CategoryName = updateTodoRequestDto.CategoryName;

            dbContext.SaveChanges();

            // Domain Model To Dto
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
        public IActionResult Delete([FromRoute] Guid id) 
        {
            var todoDomainModel = dbContext.Todos.FirstOrDefault(todo => todo.Id == id);
            
            if ( todoDomainModel == null )
            {
                return NotFound();
            }

            // Delete Todo
            dbContext.Todos.Remove(todoDomainModel);
            dbContext.SaveChanges();

            // Return Deleted Todo
            // Map Domain Model to DTO
            var todoDto = new TodoDto
            {
                Name = todoDomainModel.Name,
                Description = todoDomainModel.Description,
                // CategoryId = todoDomainModel.CategoryId,
                CategoryName = todoDomainModel.CategoryName,
            };

            return Ok();
        }

    }
}
