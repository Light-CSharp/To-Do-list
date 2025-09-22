namespace To_Do_list.Basic_logic.Interfaces
{
    public interface ITaskManager
    {
        void AddTask(Task task);
        void ReadTasks();
        void UpdateTask(int taskIndex, Task task);
        void DeleteTask(int taskIndex);
    }
}