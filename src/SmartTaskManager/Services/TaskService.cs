using SmartTaskManager.Models;
using Newtonsoft.Json;

namespace SmartTaskManager.Services;

public interface ITaskService
{
    void AddTask(TaskItem task);
    void CompleteTask(int taskId);
    List<TaskItem> GetPendingTasks();
    List<TaskItem> GetHighPriorityTasks();
    void SaveTasksToFile(string filePath);
    void LoadTasksFromFile(string filePath);
}

public class TaskService : ITaskService
{
    private readonly List<TaskItem> _tasks = new();
    private int _nextId = 1;

    public void AddTask(TaskItem task)
    {
        task.Id = _nextId++;
        _tasks.Add(task);
        Console.WriteLine($"✅ Добавлена задача: {task.Title}");
    }

    public void CompleteTask(int taskId)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == taskId);
        if (task != null)
        {
            task.IsCompleted = true;
            Console.WriteLine($"✅ Завершена задача: {task.Title}");
        }
    }

    public List<TaskItem> GetPendingTasks()
    {
        return _tasks.Where(t => !t.IsCompleted).ToList();
    }

    public List<TaskItem> GetHighPriorityTasks()
    {
        return _tasks
            .Where(t => !t.IsCompleted &&
                   (t.Priority == TaskPriority.High || t.Priority == TaskPriority.Critical))
            .ToList();
    }

    public void SaveTasksToFile(string filePath)
    {
        var json = JsonConvert.SerializeObject(_tasks, Formatting.Indented);
        File.WriteAllText(filePath, json);
        Console.WriteLine($"💾 Задачи сохранены в: {filePath}");
    }

    public void LoadTasksFromFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            var json = File.ReadAllText(filePath);
            var loadedTasks = JsonConvert.DeserializeObject<List<TaskItem>>(json);
            if (loadedTasks != null)
            {
                _tasks.Clear();
                _tasks.AddRange(loadedTasks);
                _nextId = _tasks.Count > 0 ? _tasks.Max(t => t.Id) + 1 : 1;
                Console.WriteLine($"📂 Загружено задач: {_tasks.Count}");
            }
        }
    }
}