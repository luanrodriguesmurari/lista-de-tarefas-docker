using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CRUD.Api.Interfaces;
using CRUD.Api.Models;
using CRUD.Api.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CRUD.Test
{
    public class TaskItemServiceTests
    {
        private readonly Mock<ITaskItemRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<TaskItemService>> _mockLogger;
        private readonly TaskItemService _service;

        public TaskItemServiceTests()
        {
            _mockRepository = new Mock<ITaskItemRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<TaskItemService>>();

            _service = new TaskItemService(
                _mockRepository.Object,
                _mockMapper.Object,
                _mockLogger.Object
            );
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllTaskItems()
        {
            // Arrange
            var taskItems = new List<TaskItem>
            {
                new TaskItem { Id = 1, Name = "Test Task 1" },
                new TaskItem { Id = 2, Name = "Test Task 2" }
            };
            _mockRepository.Setup(repo => repo.GetAll()).ReturnsAsync(taskItems);

            // Act
            var result = await _service.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.ToList().Count);
        }


        [Fact]
        public async Task GetById_ShouldReturnTaskItem_WhenExists()
        {
            // Arrange
            var taskItem = new TaskItem { Id = 1, Name = "Test Task 1" };
            _mockRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(taskItem);

            // Act
            var result = await _service.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Task 1", result.Name);
        }

        [Fact]
        public async Task GetById_ShouldReturnNull_WhenNotExists()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync((TaskItem)null);

            // Act
            var result = await _service.GetById(99);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Create_ShouldCallRepositoryAdd()
        {
            // Arrange
            var taskItemDto = new TaskItemDto { Name = "New Task", Description = "Task Description", Completed = false };
            var taskItem = new TaskItem { Id = 1, Name = "New Task", Description = "Task Description", Completed = false };

            _mockMapper.Setup(mapper => mapper.Map<TaskItem>(taskItemDto)).Returns(taskItem);

            // Act
            await _service.Create(taskItemDto);

            // Assert
            _mockRepository.Verify(repo => repo.Add(It.IsAny<TaskItem>()), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldUpdateTaskItem_WhenExists()
        {
            // Arrange
            var existingTask = new TaskItem { Id = 1, Name = "Old Task" };
            var updatedTaskDto = new TaskItemDto { Name = "Updated Task" };

            _mockRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(existingTask);

            // Act
            await _service.Update(1, updatedTaskDto);

            // Assert
            _mockRepository.Verify(repo => repo.Update(It.IsAny<TaskItem>()), Times.Once);
            Assert.Equal("Updated Task", existingTask.Name);
        }

        [Fact]
        public async Task Delete_ShouldCallRepositoryDelete_WhenTaskExists()
        {
            // Arrange
            var taskItem = new TaskItem { Id = 1, Name = "Test Task" };
            _mockRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(taskItem);

            // Act
            var result = await _service.Delete(1);

            // Assert
            _mockRepository.Verify(repo => repo.Delete(1), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnFalse_WhenTaskDoesNotExist()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync((TaskItem)null);

            // Act
            var result = await _service.Delete(99);

            // Assert
            Assert.False(result);
        }
    }
}
