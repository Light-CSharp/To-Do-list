using To_Do_list.Basic_logic;
using To_Do_list.Code_helper;

namespace To_Do_list.App
{
    public class App
    {
        private enum MenuChoice : byte
        {
            GoToList = 1,
            GoToFile,
            ChangePath,
            Exit = 0
        }

        private enum ListChoice : byte
        {
            AddTask = 1,
            DeleteTask,
            ReadTasks,
            UpdateTask,
            SortAscending,
            SortDescending,
            SortByPriority,
            Exit = 0
        }

        private enum FileChoice : byte
        {
            AddTask = 1,
            AddTasks,
            ReadTasks,
            WriteTask,
            WriteTasks,
            Exit = 0
        }

        static void GetPath(TaskStorage taskStorage)
        {
            Console.Write("Введите путь к текстовому файла, из которого будет браться/записываться информация: ");
            Console.WriteLine("(Если хотите проигнорировать, то напишите \'0\')");
            taskStorage.ChangePath();
        }

        static void ShowStartMenu(TaskStorage taskStorage)
        {
            Console.WriteLine("Приложение для заметок задач.");
            Console.WriteLine("Формат задачи таков: ");
            Console.WriteLine("Для списка в программе: \"Заголовок: ... | Описание: ... | Приоритет: ... | Статус: ...\".");
            Console.WriteLine("Для текстовых файлов: \"Текст заголовка | Текст описания | Текст приоритета | Текст статуса\".");
            Console.WriteLine();

            Console.WriteLine("Нюансы: ");
            Console.WriteLine("Приоритет можно задать цифрами от 1 до 3 включительно.");
            Console.WriteLine("Сортировка задач лишь показывает отсортированные задачи, а не изменяет список.");
            Console.WriteLine("Если не удастся обработать задачу из файла, то действие прервётся.");
            Console.WriteLine();

            GetPath(taskStorage);
        }

        static void HandleListMenu(TaskManager taskManager)
        {
            Console.WriteLine("Действия со списком: ");
            Console.WriteLine("1. Добавить задачу в список задач.");
            Console.WriteLine("2. Удалить задачу.");
            Console.WriteLine("3. Просмотреть список задач.");
            Console.WriteLine("4. Обновить задачу.");
            Console.WriteLine("5. Сортировка списка задач по возрастанию.");
            Console.WriteLine("6. Сортировка списка задач по уменьшению.");
            Console.WriteLine("7. Сортировка списка задач по заданному приоритету.");
            Console.WriteLine("0. Выйти.");

            Console.Write("Выберите номер действия: ");
            int choice = Validator.GetIntInRange(0, 7);

            Console.Clear();
            switch ((ListChoice)choice)
            {
                case ListChoice.AddTask:
                    taskManager.AddTask(TaskManager.GetTaskFromUser());
                    break;

                case ListChoice.DeleteTask:
                    taskManager.DeleteTask();
                    break;

                case ListChoice.ReadTasks:
                    taskManager.ReadTasks();
                    break;

                case ListChoice.UpdateTask:
                    taskManager.UpdateTask();
                    break;

                case ListChoice.SortAscending:
                    taskManager.SortTasks(sortOption: SortOption.Ascending);
                    break;

                case ListChoice.SortDescending:
                    taskManager.SortTasks(sortOption: SortOption.Descending);
                    break;

                case ListChoice.SortByPriority:
                    Console.Write("Введите приоритет задачи (1-3): ");

                    taskManager.SortTasks(taskPriority: (TaskPriority)Validator.GetIntInRange(1, 3));
                    break;

                case ListChoice.Exit:
                    return;
            }
        }

        static void HandleFileMenu(TaskStorage taskStorage, TaskManager taskManager)
        {
            if (string.IsNullOrWhiteSpace(taskStorage.Path))
            {
                MessageAssistant.RedMessage("Нет пути к файлу!");
                return;
            }

            Console.WriteLine("Действия с файлом: ");
            Console.WriteLine("1. Добавить задачу из файла.");
            Console.WriteLine("2. Добавить все задачи из файла.");
            Console.WriteLine("3. Просмотреть задачи из файла.");
            Console.WriteLine("4. Записать задачу в файл.");
            Console.WriteLine("5. Записать все задачи в файл.");
            Console.WriteLine("0. Выйти");

            Console.Write("Выберите номер действия: ");
            int choice = Validator.GetIntInRange(0, 5);

            Console.Clear();
            switch ((FileChoice)choice)
            {
                case FileChoice.AddTask:
                    Task task = taskStorage.GetTask()!;
                    if (task == null)
                    {
                        return;
                    }

                    taskManager.AddTask(task);
                    break;

                case FileChoice.AddTasks:
                    if (taskStorage.GetTasks() == null)
                    {
                        return;
                    }

                    List<Task> tasks = taskStorage.GetTasks()!;
                    if (tasks.Count == 0)
                    {
                        return;
                    }

                    foreach (Task item in tasks)
                    {
                        taskManager.AddTask(item);
                    }
                    break;

                case FileChoice.ReadTasks:
                    taskStorage.ReadTasks();
                    break;

                case FileChoice.WriteTask:
                    if (taskManager.GetTasks.Count == 0)
                    {
                        MessageAssistant.RedMessage("Нечего записывать, коллекция пуста!");
                        return;
                    }

                    taskStorage.WriteTask(taskManager.GetTasks[taskManager.GetTaskIndex()]);
                    break;

                case FileChoice.WriteTasks:
                    if (taskManager.GetTasks.Count == 0)
                    {
                        MessageAssistant.RedMessage("Нечего записывать, коллекция пуста!");
                        return;
                    }

                    taskStorage.WriteTasks(taskManager.GetTasks);
                    break;

                case FileChoice.Exit:
                    return;
            }
        }

        static void ShowMenu(TaskManager taskManager, TaskStorage taskStorage)
        {
            Console.WriteLine("1. Работа со списком задач");
            Console.WriteLine("2. Работа с файлом");
            Console.WriteLine("3. Сменить путь к файлу");
            Console.WriteLine("0. Выйти");

            Console.Write("Выберите номер действия: ");
            int choice = Validator.GetIntInRange(0, 3);

            Console.Clear();
            switch ((MenuChoice)choice)
            {
                case MenuChoice.GoToList:
                    HandleListMenu(taskManager);
                    break;

                case MenuChoice.GoToFile:
                    HandleFileMenu(taskStorage, taskManager);
                    break;

                case MenuChoice.ChangePath:
                    GetPath(taskStorage);
                    break;

                case MenuChoice.Exit:
                    Environment.Exit(0);
                    break;
            }
        }

        static void Main()
        {
            TaskManager taskManager = new();
            TaskStorage taskStorage = new();

            ShowStartMenu(taskStorage);
            while (true)
            {
                Console.WriteLine();

                ShowMenu(taskManager, taskStorage);
            }
        }
    }
}