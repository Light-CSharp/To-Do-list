namespace To_Do_list
{
    public enum TaskPriority : byte
    {
        Low = 1,
        Medium,
        High
    }

    public class Task(string title, string description, TaskPriority taskPriority, bool isCompleted)
    {
        private static int totalId = 0;

        public string Title { get; set; } = title;
        public string Description { get; set; } = description;
        public TaskPriority TaskPriority { get; set; } = taskPriority;
        public bool IsCompleted { get; set; } = isCompleted;

        public int Id { get; init; } = ++totalId;

        public string GetInfo =>
            $"Заголовок: \"{Title}\" | Описание: \"{Description}\" | Приоритет: {TaskPriority}" +
            $" | Статус: {(IsCompleted ? "Выполнена" : "Не выполнена")}";

        public Task(string title, string description, TaskPriority taskPriority) :
            this(title, description, taskPriority, false)
        { }
        public Task(string title, string description) : this(title, description, TaskPriority.Low, false) { }
    }
}