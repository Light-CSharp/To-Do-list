#pragma warning disable IDE0028
#pragma warning disable IDE0306

using To_Do_list.Basic_logic.Interfaces;
using To_Do_list.Code_helper;

namespace To_Do_list.Basic_logic
{
    public enum SortOption : byte
    {
        Ascending = 1,
        Descending
    }

    public class TaskManager : ITaskManager
    {
        private readonly List<Task> tasks = [];

        public List<Task> GetTasks => new(tasks);

        private bool HasTasks()
        {
            if (tasks.Count == 0)
            {
                MessageAssistant.RedMessage("Коллекция записей пуста!");
                return false;
            }

            return true;
        }

        public int GetTaskIndex()
        {
            PrintTasks(tasks);

            Console.Write("Выберите ID задачи: ");
            int taskIndex = Validator.GetIntInRange(1, tasks.Count) - 1; // Из номера задачи вычитаем 1, чтобы узнать индекс задачи. 

            return taskIndex;
        }

        private static void PrintTasks(List<Task> tasks)
        {
            MessageAssistant.BlueMessage("Список задач: \n");
            foreach (Task task in tasks)
            {
                MessageAssistant.BlueMessage($"ID: {task.Id}. {task.GetInfo}\n");
            }
            Console.WriteLine();
        }

        public static Task GetTaskFromUser()
        {
            Console.Write("Введите заголовок: ");
            string title = Validator.GetString();

            Console.Write("Введите описание: ");
            string description = Validator.GetString();

            Console.Write("Введите приоритет (1 до 3 включительно, где 3 - важнейшая. 0 для пропуска, но закончит заполнение информации): ");
            int priority = Validator.GetIntInRange(0, 3);
            if (priority == 0)
            {
                return new Task(title, description);
            }

            Console.Write("Статус выполненной задачи (Да/Нет или любое другое выражение для пропуска): ");
            string answer = Validator.GetString();
            bool isCompleted = answer.Equals("да", StringComparison.OrdinalIgnoreCase);

            return new Task(title, description, (TaskPriority)priority, isCompleted);
        }

        public void AddTask(Task task)
        {
            tasks.Add(task);
        }

        public void DeleteTask()
        {
            if (!HasTasks())
            {
                return;
            }

            tasks.RemoveAt(GetTaskIndex());
        }

        public void ReadTasks()
        {
            if (!HasTasks())
            {
                return;
            }

            PrintTasks(tasks);
        }

        public void UpdateTask()
        {
            if (!HasTasks())
            {
                return;
            }

            tasks[GetTaskIndex()] = GetTaskFromUser();
        }

        public void SortTasks(SortOption sortOption)
        {
            if (!HasTasks())
            {
                return;
            }

            switch (sortOption)
            {
                case SortOption.Ascending:
                    AscendingSort();
                    break;

                case SortOption.Descending:
                    DescendingSort();
                    break;
            }
        }

        public void SortTasks(TaskPriority taskPriority)
        {
            if (!HasTasks())
            {
                return;
            }

            ByPrioritySort(taskPriority);
        }

        private void AscendingSort()
        {
            List<Task> ascendingTasks = new(tasks);
            for (int i = 0; i < ascendingTasks.Count; i++)
            {
                for (int j = 0; j < ascendingTasks.Count - i - 1; j++)
                {
                    if (ascendingTasks[j].TaskPriority > ascendingTasks[j + 1].TaskPriority)
                    {
                        (ascendingTasks[j], ascendingTasks[j + 1]) = (ascendingTasks[j + 1], ascendingTasks[j]);
                    }
                }
            }

            PrintTasks(ascendingTasks);
        }

        private void DescendingSort()
        {
            List<Task> descendingTasks = new(tasks);
            for (int i = 1; i < descendingTasks.Count; i++)
            {
                Task key = descendingTasks[i];
                int j = i - 1;

                while (j >= 0 && descendingTasks[j].TaskPriority < key.TaskPriority)
                {
                    descendingTasks[j + 1] = descendingTasks[j];
                    j--;
                }

                descendingTasks[j + 1] = key;
            }

            PrintTasks(descendingTasks);
        }

        private void ByPrioritySort(TaskPriority taskPriority)
        {
            List<Task> byPriorityTasks = [];
            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].TaskPriority == taskPriority)
                {
                    byPriorityTasks.Add(tasks[i]);
                }
            }

            PrintTasks(byPriorityTasks);
        }
    }
}