using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PerfectChannel.WebApi.Controllers;
using PerfectChannel.WebApi.Data;
using PerfectChannel.WebApi.Data.Entities;
using PerfectChannel.WebApi.Mappers;
using PerfectChannel.WebApi.Models;
using Xunit;

namespace PerfectChannel.WebApi.Test.Controllers
{
    public class TodoControllerTest
    {
        
        [Fact]
        public async Task Returns_ListOf_Completed()
        {
            // Arrange
            var mockRepo = new Mock<ITodosRepository>();
            mockRepo.Setup(repo => repo.GetByStatus(true))
                .Returns(GetCompletedTodos());

            var controller = new TodoController(mockRepo.Object, GetMapper());

            // Act
            var result = await controller.GetCompleted();

            // Assert
            Assert.Equal(1, result.Value[0].Id);
            Assert.Equal("Test One", result.Value[0].Description);
        }

        [Fact]
        public async Task Returns_ListOf_Pending()
        {
            // Arrange
            var mockRepo = new Mock<ITodosRepository>();
            mockRepo.Setup(repo => repo.GetByStatus(false))
                .Returns(GetPendingTodos());

            var controller = new TodoController(mockRepo.Object, GetMapper());

            // Act
            var result = await controller.GetPending();

            // Assert
            Assert.Equal(1, result.Value[0].Id);
            Assert.Equal("Test One", result.Value[0].Description);
        }

        [Fact]
        public void Create_ReturnsBadRequest_GivenNullModel()
        {
            // Arrange & Act
            var mockRepo = new Mock<ITodosRepository>();
            var controller = new TodoController(mockRepo.Object, GetMapper());
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = controller.CreateTodo(todoModel: null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void Create_ReturnsCreatedTodo_GivenCorrectInputs()
        {
            // Arrange
            var mockRepo = new Mock<ITodosRepository>();
            var controller = new TodoController(mockRepo.Object, GetMapper());

            // Act
            var result = controller.CreateTodo(GetTodoModel());

            // Assert
            var okResult = Assert.IsType<CreatedAtRouteResult>(result.Result);
            var returnTodo = Assert.IsType<TodoModel>(okResult.Value);
            Assert.Equal(GetTodoModel().Description, returnTodo.Description);
            Assert.Equal(GetTodoModel().IsDone, returnTodo.IsDone);
        }

        [Fact]
        public void Update_ReturnsBadRequest_GivenNullModel()
        {
            // Arrange
            var mockRepo = new Mock<ITodosRepository>();
            var controller = new TodoController(mockRepo.Object,GetMapper());
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = controller.UpdateTodo(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void Update_ReturnsHttpNotFound_ForInvalidId()
        {
            // Arrange
            var todoModel = GetTodoModel();
            todoModel.Id = 123;
            var mockRepo = new Mock<ITodosRepository>();
            var controller = new TodoController(mockRepo.Object, GetMapper());
            // Act
            var result = controller.UpdateTodo(todoModel);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        private static IMapper GetMapper()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TodoMapper());
            });
            var mapper = mockMapper.CreateMapper();
            return mapper;
        }

        private static TodoModel GetTodoModel()
        {
            return new TodoModel()
            {
                Description = "Test One",
                IsDone = false
            };
        }

        private static async Task<Todo[]> GetCompletedTodos()
        {
            return new Todo[]
            {
                new Todo
                {
                    Id = 1,
                    Description = "Test One",
                    IsDone = true
                },
                new Todo
                {
                    Id = 2,
                    Description = "Test Two",
                    IsDone = true
                }
            };
        }
        private static async Task<Todo[]> GetPendingTodos()
        {
            return new Todo[]
            {
                new Todo
                {
                    Id = 1,
                    Description = "Test One",
                    IsDone = false
                },
                new Todo
                {
                    Id = 2,
                    Description = "Test Two",
                    IsDone = false
                }
            };
        }
    }
}
