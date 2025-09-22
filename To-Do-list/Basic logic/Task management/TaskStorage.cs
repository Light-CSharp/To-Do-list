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

        private string[] ReadFileLines()
        {
            if (!HasPath())
            {
                return [];
            }

            return File.ReadAllLines(Path, Encoding.UTF8);
        }

        private 

        public Task? AddTask(Task task)
        {
            if (!HasPath())
            {
                return null;
            }

            MessageAssistant.BlueMessage("Список из файла: ");

            string[] lines = File.ReadAllLines(Path, Encoding.UTF8);
            for (int i = 0; i < lines.Length; i++)
            {
                MessageAssistant.BlueMessage($"Номер: {i + 1}. {lines[i]}");
            }

            Console.WriteLine("Выберите номер задачи: ");
            int lineIndex = Validator.GetIntInRange(1, lines.Length) - 1; // Из номера задачи вычитаем 1, чтобы узнать индекс задачи. 

            string line = lines[lineIndex];
            if (!Validator.IsTask(line))
            {
                Console.WriteLine("Данная строка из файла не подходит по формату!");
                return null;
            }

            string[] data = line.Split('|');
            return new Task(data[0], data[1], (TaskPriority)Enum.Parse(typeof(TaskPriority), data[2], ignoreCase: true), bool.Parse(data[3]));
        }


        public List<Task>? AddTasks(List<Task> tasks)
        {
            if (!HasPath())
            {
                return null;
            }
        }

        public void ReadTasks()
        {
            if (!HasPath())
            {
                return;
            }
        }
    }
}