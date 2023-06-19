using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IMapper mapper;

        public TodosController(TodoListDbContext dbContext, ITodoRepository todoRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.todoRepository = todoRepository;
            this.mapper = mapper;
        }

        // GET ALL TODOS
        // Get: https://localhost:7082/api/todos
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {   
            // Get Data From Database - Domain Models
            var todosDomain = await todoRepository.GetAllAsync();

            // Return DTO
            return Ok(mapper.Map<List<TodoDto>>(todosDomain));
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
            
            // Return DTO
            return Ok(mapper.Map<TodoDto>(todoDomain));
        }

        // POST To Create New TODO
        // POST: https://localhost:7082/api/todos/
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddTodoRequestDto addTodoRequestDto)
        {
            // Mapo Dto to Domain Model
            var todoDomainModel = mapper.Map<Todo>(addTodoRequestDto);

            // Use Domain Model to Create Todo
            todoDomainModel = await todoRepository.CreateAsync(todoDomainModel);

            // Map Domain to DTO
            var todoDto = mapper.Map<TodoDto>(todoDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = todoDto.Id }, todoDto);
        }

        // PUT To Update TODO
        // PUT: https://localhost:7082/api/todos/{id}
        [HttpPut]
        [Route("id:Guid")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateTodoRequestDto updateTodoRequestDto )
        {
            // Map DTO To Domain Model
            var todoDomainModel = mapper.Map<Todo>(updateTodoRequestDto);

            // Updates Todo or Returns null
            todoDomainModel = await todoRepository.UpdateAsync(id, todoDomainModel);

            if ( todoDomainModel == null )
            {
                return NotFound();
            }

            // Return DTO
            return Ok(mapper.Map<TodoDto>(todoDomainModel));
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

            return Ok(mapper.Map<TodoDto>(todoDomainModel));
        }

    }
}
