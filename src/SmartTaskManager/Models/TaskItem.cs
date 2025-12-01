namespace SmartTaskManager.Models;

public enum TaskPriority
{
    Low,
    Medium,
    High,
    Critical
}

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskPriority Priority { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? DueDate { get; set; }
    public bool IsCompleted { get; set; }

    public override string ToString()
    {
        var status = IsCompleted ? "✅" : "⏳";
        return $"{status} [{Priority}] {Title} - {Description}";
    }
}