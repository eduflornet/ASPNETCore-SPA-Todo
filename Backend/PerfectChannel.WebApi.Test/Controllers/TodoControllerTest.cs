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
            var result = controller.CreateTodo(todo: null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void Create_ReturnsCreatedTodo_GivenCorrectInputs()
        {
            // Arrange
            const string description = "Test One";
            const bool isDone = false;
            var todoModel = new TodoModel()
            {
                Description = description,
                IsDone = isDone
            };
            var mockRepo = new Mock<ITodosRepository>();
            var controller = new TodoController(mockRepo.Object, GetMapper());

            // Act
            var result = controller.CreateTodo(todoModel);

            // Assert
            var okResult = Assert.IsType<CreatedAtRouteResult>(result.Result);
            var returnTodo = Assert.IsType<TodoModel>(okResult.Value);
            Assert.Equal(description, returnTodo.Description);
            Assert.Equal(isDone, returnTodo.IsDone);
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
