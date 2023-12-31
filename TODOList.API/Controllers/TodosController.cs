﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TODOList.API.Models.Domain;
using TODOList.API.Models.DTO;
using TODOList.API.Repositories;
using TODOList.API.CustomActionFilters;

namespace TODOList.API.Controllers
{
    // https://localhost:7082/api/todos
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoRepository todoRepository;
        private readonly IMapper mapper;

        public TodosController(ITodoRepository todoRepository, IMapper mapper)
        {
            this.todoRepository = todoRepository;
            this.mapper = mapper;
        }

        // GET ALL TODOS
        // Get: https://localhost:7082/api/todos?filterOn=Name&Query=Patusco&sortBy=Name&IsAscending=true&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortby, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            // Get Data From Database - Domain Models
            var todosDomain = await todoRepository.GetAllAsync(filterOn, filterQuery, sortby, isAscending ?? true,
                pageNumber, pageSize);

            // Return DTO
            return Ok(mapper.Map<List<TodoDto>>(todosDomain));
        }

        // GET SPECIFIC TODO (Get Todo By ID)
        // Get: https://localhost:7082/api/todos/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // Get Data From Database - Domain Models
            var todoDomain = await todoRepository.GetByIdAsync(id);

            if (todoDomain == null)
            {
                return NotFound();
            }

            // Return DTO
            return Ok(mapper.Map<TodoDto>(todoDomain));
        }

        // POST To Create New TODO
        // POST: https://localhost:7082/api/todos/
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddTodoRequestDto addTodoRequestDto)
        {
            // Mapo DTO to Domain Model
            var todoDomainModel = mapper.Map<Todo>(addTodoRequestDto);

            // Use Domain Model to Create Todo
            await todoRepository.CreateAsync(todoDomainModel, addTodoRequestDto.CategoryIds);

            var todoDto = mapper.Map<TodoDto>(todoDomainModel);

            // Return DTO
            return Ok(todoDto);
        }

        // PUT To Update TODO
        // PUT: https://localhost:7082/api/todos/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateTodoRequestDto updateTodoRequestDto)
        {
            // Map DTO To Domain Model
            var todoDomainModel = mapper.Map<Todo>(updateTodoRequestDto);

            // Updates Todo or Returns null
            var updatedDomainModel = await todoRepository.UpdateAsync(id, todoDomainModel, updateTodoRequestDto.CategoryIds);

            if (updatedDomainModel == null)
            {
                return NotFound();
            }

            // Return DTO
            return Ok(mapper.Map<TodoDto>(updatedDomainModel));
        }

        // DELETE TODO
        // DELETE: https://localhost:7082/api/todos/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // Deletes TODO or Returns null
            var todoDomainModel = await todoRepository.DeleteAsync(id);

            if (todoDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<TodoDto>(todoDomainModel));
        }
    }
}
