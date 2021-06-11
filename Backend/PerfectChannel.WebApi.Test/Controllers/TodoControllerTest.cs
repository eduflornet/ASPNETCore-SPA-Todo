using System.Threading.Tasks;
using AutoMapper;
using Moq;
using PerfectChannel.WebApi.Controllers;
using PerfectChannel.WebApi.Data;
using PerfectChannel.WebApi.Data.Entities;
using PerfectChannel.WebApi.Mappers;
using Xunit;

namespace PerfectChannel.WebApi.Test.Controllers
{
    public class TodoControllerTest
    {
        
        [Fact]
        public async Task Index_Returns_ListOf_Completed()
        {
            // Arrange
            var mockRepo = new Mock<ITodosRepository>();
            mockRepo.Setup(repo => repo.GetByStatus(true))
                .Returns(GetCompletedTodos());

            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TodoMapper());
            });
            var mapper = mockMapper.CreateMapper();

            var controller = new TodoController(mockRepo.Object, mapper);

            // Act
            var result = await controller.GetCompleted();

            // Assert
            Assert.Equal(1, result.Value[0].Id);
            Assert.Equal("Test One", result.Value[0].Description);
        }

        [Fact]
        public async Task Index_Returns_ListOf_Pending()
        {
            // Arrange
            var mockRepo = new Mock<ITodosRepository>();
            mockRepo.Setup(repo => repo.GetByStatus(false))
                .Returns(GetPendingTodos());

            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TodoMapper());
            });
            var mapper = mockMapper.CreateMapper();

            var controller = new TodoController(mockRepo.Object, mapper);

            // Act
            var result = await controller.GetPending();

            // Assert
            Assert.Equal(1, result.Value[0].Id);
            Assert.Equal("Test One", result.Value[0].Description);
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
