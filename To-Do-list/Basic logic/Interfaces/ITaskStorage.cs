namespace To_Do_list.Basic_logic.Interfaces
{
    public interface ITaskStorage
    {
        Task? GetTaskFromFile();
        List<Task>? GetTasksFromFile(List<Task> tasks);
        void ReadTasks();
    }
}