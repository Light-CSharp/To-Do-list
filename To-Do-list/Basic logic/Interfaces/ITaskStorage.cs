namespace To_Do_list.Basic_logic.Interfaces
{
    public interface ITaskStorage
    {
        Task? GetTask();
        List<Task>? GetTasks();
        void ReadTasks();
        void WriteTask(Task task);
        void WriteTasks(List<Task> tasks);
    }
}