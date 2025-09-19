using To_Do_list.Basic_logic.Interfaces;
using To_Do_list.Code_helper;

namespace To_Do_list.Basic_logic
{
    public class TaskManager : ITaskManager
    {
        private readonly List<Task> tasks = [];

        private bool HasTasks()
        {
            if (tasks.Count == 0)
            {
                MessageAssistant.RedMessage("Коллекция записей пуста!");
                return false;
            }

            return true;
        }

        private int GetTaskIndex()
        {
            MessageAssistant.BlueMessage("Список задач: ");
            foreach (Task task in tasks)
            {
                MessageAssistant.BlueMessage($"ID: {task.Id}. {task.GetInfo}");
            }

            Console.WriteLine("Выберите ID задачи: ");
            int taskIndex = Validator.GetIntInRange(1, tasks.Count) - 1; // // Из номера задачи вычитаем 1, чтобы узнать индекс задачи. 

            return taskIndex;
        }

        public void AddTask(Task task)
        {
            tasks.Add(task);
        }

        public void DeleteTask(int taskIndex)
        {
            if (!HasTasks())
            {
                return;
            }

            taskIndex = GetTaskIndex();
            tasks.RemoveAt(taskIndex);
        }

        public void ReadTasks()
        {
            if (!HasTasks())
            {
                return;
            }

            MessageAssistant.BlueMessage("Список задач: ");
            foreach (Task task in tasks)
            {
                MessageAssistant.BlueMessage($"ID: {task.Id}. {task.GetInfo}");
            }
        }

        public void UpdateTask(int taskIndex, Task task)
        {
            if (!HasTasks())
            {
                return;
            }

            taskIndex = GetTaskIndex();
            tasks[taskIndex] = task;
        }
    }
}