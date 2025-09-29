using System.Text;
using To_Do_list.Basic_logic.Interfaces;
using To_Do_list.Code_helper;

namespace To_Do_list.Basic_logic
{
    public class TaskStorage() : ITaskStorage
    {
        public string Path { get; private set; } = default!;

        private bool HasPath()
        {
            if (string.IsNullOrEmpty(Path))
            {
                MessageAssistant.RedMessage("Чтобы использовать действия с файлом нужно указать путь для них!");
                return false;
            }

            return true;
        }

        private void PrintTasksInFile()
        {
            MessageAssistant.BlueMessage("Список из файла: ");

            string[] lines = File.ReadAllLines(Path, Encoding.UTF8);
            for (int i = 0; i < lines.Length; i++)
            {
                MessageAssistant.BlueMessage($"Номер: {i + 1}. {lines[i]}");
            }
        }

        public void ChangePath()
        {
            bool isExist;
            string newPath;

            do
            {
                newPath = Validator.GetString();
                isExist = File.Exists(newPath);
                if (!isExist)
                {
                    MessageAssistant.RedMessage("Файла под таким путём не существует! Попробуйте снова: ");
                }
            } while (!isExist);

            Path = newPath;
        }

        public Task? GetTaskFromFile()
        {
            if (!HasPath())
            {
                return null;
            }

            string[] lines = File.ReadAllLines(Path, Encoding.UTF8);
            PrintTasksInFile();

            Console.WriteLine("Выберите номер задачи: ");
            int lineIndex = Validator.GetIntInRange(1, lines.Length) - 1; // Из номера задачи вычитаем 1, чтобы узнать индекс задачи. 

            return Validator.GetTask(lines[lineIndex]);
        }


        public List<Task>? GetTasksFromFile(List<Task> tasks)
        {
            if (!HasPath())
            {
                return null;
            }
            
            PrintTasksInFile();
            string[] lines = File.ReadAllLines(Path, Encoding.UTF8);

            List<Task> tasksFromFile = [];
            foreach (string task in lines)
            {
                Task? newTask = Validator.GetTask(task);
                if (newTask == null)
                {
                    MessageAssistant.RedMessage("Добавление задач из файла прервано!");
                    return null;
                }

                tasksFromFile.Add(newTask);
            }

            return tasksFromFile;
        }

        public void ReadTasks()
        {
            if (!HasPath())
            {
                return;
            }

            PrintTasksInFile();
        }
    }
}