namespace To_Do_list.Basic_logic.Interfaces
{
    public interface ITaskManager
    {
        void AddTask(Task task);
        void ReadTasks();
        void UpdateTask();
        void DeleteTask();
    }
}