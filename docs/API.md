# Smart Task Manager API Documentation

## Сервисы

### TaskService
Методы для управления задачами:
- `AddTask()` - добавление новой задачи
- `CompleteTask()` - завершение задачи
- `GetPendingTasks()` - получение активных задач
- `GetHighPriorityTasks()` - получение задач высокого приоритета

### TaskAnalyzer
Анализ состояния задач и генерация отчетов.

## Пример использования

```csharp
var taskService = new TaskService();
var analyzer = new TaskAnalyzer(taskService);

// Добавление задачи
taskService.AddTask(new TaskItem { Title = "Task", Priority = TaskPriority.High });

// Анализ
var report = analyzer.AnalyzeTasks();
report.PrintReport();