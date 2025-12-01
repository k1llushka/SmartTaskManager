using SmartTaskManager.Models;
using SmartTaskManager.Services;
using Xunit;

namespace SmartTaskManager.Tests.IntegrationTests;

[Trait("Category", "Integration")]
public class TaskAnalyzerTests
{
    [Fact]
    public void AnalyzeTasks_WithMixedTasks_ReturnsCorrectAnalysis()
    {
        // Arrange
        var taskService = new TaskService();
        var analyzer = new TaskAnalyzer(taskService);

        taskService.AddTask(new TaskItem { Title = "Critical", Priority = TaskPriority.Critical });
        taskService.AddTask(new TaskItem { Title = "High", Priority = TaskPriority.High });
        taskService.AddTask(new TaskItem { Title = "Low", Priority = TaskPriority.Low });

        // Act
        var result = analyzer.AnalyzeTasks();

        // Assert
        Assert.Equal(3, result.TotalTasks);
        Assert.Equal(2, result.HighPriorityCount);
        Assert.True(result.HasCriticalTasks);
    }
}