using SmartTaskManager.Models;

namespace SmartTaskManager.Services;

public class TaskAnalyzer
{
    private readonly ITaskService _taskService;

    public TaskAnalyzer(ITaskService taskService)
    {
        _taskService = taskService;
    }

    public AnalysisResult AnalyzeTasks()
    {
        var pendingTasks = _taskService.GetPendingTasks();
        var highPriorityTasks = _taskService.GetHighPriorityTasks();

        return new AnalysisResult
        {
            TotalTasks = pendingTasks.Count,
            HighPriorityCount = highPriorityTasks.Count,
            UrgentTasks = highPriorityTasks.Where(t =>
                t.DueDate.HasValue && t.DueDate.Value.Date == DateTime.Today).ToList(),
            HasCriticalTasks = highPriorityTasks.Any(t => t.Priority == TaskPriority.Critical)
        };
    }

    public class AnalysisResult
    {
        public int TotalTasks { get; set; }
        public int HighPriorityCount { get; set; }
        public List<TaskItem> UrgentTasks { get; set; } = new();
        public bool HasCriticalTasks { get; set; }

        public void PrintReport()
        {
            Console.WriteLine("📊 Отчет по задачам:");
            Console.WriteLine($"   Всего задач: {TotalTasks}");
            Console.WriteLine($"   Высокий приоритет: {HighPriorityCount}");
            Console.WriteLine($"   Срочные на сегодня: {UrgentTasks.Count}");
            Console.WriteLine($"   Критические задачи: {(HasCriticalTasks ? "Есть" : "Нет")}");
        }
    }
}