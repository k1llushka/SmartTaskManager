using SmartTaskManager.Models;
using SmartTaskManager.Services;
using Xunit;

namespace SmartTaskManager.Tests.UnitTests;

[Trait("Category", "Unit")]
public class TaskServiceTests
{
    private readonly ITaskService _taskService;

    public TaskServiceTests()
    {
        _taskService = new TaskService();
    }

    [Fact]
    public void AddTask_ValidTask_AddsSuccessfully()
    {
        // Arrange
        var task = new TaskItem { Title = "Test Task", Priority = TaskPriority.Medium };

        // Act
        _taskService.AddTask(task);

        // Assert
        var pendingTasks = _taskService.GetPendingTasks();
        Assert.Single(pendingTasks);
        Assert.Equal("Test Task", pendingTasks[0].Title);
    }

    [Fact]
    public void CompleteTask_ExistingTask_MarksAsCompleted()
    {
        // Arrange
        var task = new TaskItem { Title = "Complete Me", Priority = TaskPriority.Low };
        _taskService.AddTask(task);
        var taskId = _taskService.GetPendingTasks()[0].Id;

        // Act
        _taskService.CompleteTask(taskId);

        // Assert
        var pendingTasks = _taskService.GetPendingTasks();
        Assert.Empty(pendingTasks);
    }

    [Fact]
    public void GetHighPriorityTasks_ReturnsOnlyHighAndCritical()
    {
        // Arrange
        _taskService.AddTask(new TaskItem { Title = "High", Priority = TaskPriority.High });
        _taskService.AddTask(new TaskItem { Title = "Critical", Priority = TaskPriority.Critical });
        _taskService.AddTask(new TaskItem { Title = "Low", Priority = TaskPriority.Low });

        // Act
        var highPriorityTasks = _taskService.GetHighPriorityTasks();

        // Assert
        Assert.Equal(2, highPriorityTasks.Count);
        Assert.Contains(highPriorityTasks, t => t.Title == "High");
        Assert.Contains(highPriorityTasks, t => t.Title == "Critical");
        Assert.DoesNotContain(highPriorityTasks, t => t.Title == "Low");
    }
}