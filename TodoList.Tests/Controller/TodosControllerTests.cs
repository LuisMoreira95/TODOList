using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOList.API.Controllers;
using TODOList.API.Models.Domain;
using TODOList.API.Models.DTO;
using TODOList.API.Repositories;

namespace TodoList.Tests.Controller
{
    public class TodosControllerTests
    {
        private readonly ITodoRepository todoRepository;
        private readonly IMapper mapper;
        public TodosControllerTests()
        {
            this.todoRepository = A.Fake<ITodoRepository>();
            this.mapper = A.Fake<IMapper>();
        }

        [Fact]
        public async void GetAll_ReturnOk()
        {
            // Arrange
            var todosDomain = A.Fake<List<Todo>>();

            A.CallTo(() => mapper.Map<List<TodoDto>>(todosDomain)).Returns(A.Fake<List<TodoDto>>());

            var controller = new TodosController(todoRepository, mapper);

            // Act
            var result = await controller.GetAll(null, null, null, null);
            
            // Assert
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async void GetById_ReturnOk()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            var todoDomain = A.Fake<Todo>();

            A.CallTo(() => mapper.Map<TodoDto>(todoDomain)).Returns(A.Fake<TodoDto>());

            var controller = new TodosController(todoRepository, mapper);

            // Act
            var result = await controller.GetById(id);

            // Assert
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async void Create_ReturnOk()
        {
            // Arrange
            var todoRequest = A.Fake<AddTodoRequestDto>();
            
            var todoDomain = A.CallTo(() => mapper.Map<Todo>(todoRequest));

            A.CallTo(() => mapper.Map<TodoDto>(todoDomain)).Returns(A.Fake<TodoDto>());

            var controller = new TodosController(todoRepository, mapper);

            // Act
            var result = await controller.Create(todoRequest);

            // Assert
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async void Update_ReturnOk()
        {
            // Arrange
            var id = Guid.NewGuid();

            var todoRequest = A.Fake<UpdateTodoRequestDto>();

            var todoDomain = A.CallTo(() => mapper.Map<Todo>(todoRequest));

            A.CallTo(() => mapper.Map<TodoDto>(todoDomain)).Returns(A.Fake<TodoDto>());

            var controller = new TodosController(todoRepository, mapper);

            // Act
            var result = await controller.Update(id, todoRequest);

            // Assert
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async void Delete_ReturnOk()
        {
            // Arrange
            var id = Guid.NewGuid();

            var todoDomain = A.Fake<Todo>();

            A.CallTo(() => mapper.Map<TodoDto>(todoDomain)).Returns(A.Fake<TodoDto>());

            var controller = new TodosController(todoRepository, mapper);

            // Act
            var result = await controller.Delete(id);

            // Assert
            result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}
