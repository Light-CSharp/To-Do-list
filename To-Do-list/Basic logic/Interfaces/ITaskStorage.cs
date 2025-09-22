namespace To_Do_list.Basic_logic.Interfaces
{
    public interface ITaskStorage
    {
        Task? AddTask(Task task);
        List<Task>? AddTasks(List<Task> tasks);
        void ReadTasks();
    }
}