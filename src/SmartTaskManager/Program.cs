using SmartTaskManager.Models;
using SmartTaskManager.Services;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<ITaskService, TaskService>();
services.AddTransient<TaskAnalyzer>();

var serviceProvider = services.BuildServiceProvider();
var taskService = serviceProvider.GetRequiredService<ITaskService>();
var analyzer = serviceProvider.GetRequiredService<TaskAnalyzer>();

// Добавляем тестовые задачи
taskService.AddTask(new TaskItem
{
    Title = "Настроить CI/CD пайплан",
    Description = "Добавить кэширование и условия",
    Priority = TaskPriority.High
});

taskService.AddTask(new TaskItem
{
    Title = "Написать документацию",
    Description = "Описание API методов",
    Priority = TaskPriority.Medium
});

taskService.AddTask(new TaskItem
{
    Title = "Исправить критический баг",
    Description = "Падение при загрузке файлов",
    Priority = TaskPriority.Critical,
    DueDate = DateTime.Today
});

// Анализируем задачи
var result = analyzer.AnalyzeTasks();
result.PrintReport();

// Сохраняем задачи
taskService.SaveTasksToFile("tasks.json");

Console.WriteLine("\n🚀 Приложение Smart Task Manager запущено успешно!");