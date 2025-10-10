#pragma warning disable IDE0063

using System.Text;
using To_Do_list.Basic_logic.Interfaces;
using To_Do_list.Code_helper;

namespace To_Do_list.Basic_logic
{
    public class TaskStorage() : ITaskStorage
    {
        private const string INTERRUPT_VALUE = "0";

        public string Path { get; private set; } = default!;
        
        private void PrintTasks()
        {
            MessageAssistant.BlueMessage("Список из файла: \n");

            string[] lines = File.ReadAllLines(Path, Encoding.UTF8);
            for (int i = 0; i < lines.Length; i++)
            {
                MessageAssistant.BlueMessage($"Номер: {i + 1}. {lines[i]}\n");
            }
            Console.WriteLine();
        }

        public void ChangePath()
        {
            bool isExist;
            string newPath;

            do
            {
                newPath = Validator.GetString();
                if (newPath == INTERRUPT_VALUE) 
                { 
                    return; 
                }

                isExist = File.Exists(newPath);
                if (!isExist)
                {
                    MessageAssistant.RedMessage("Файла под таким путём не существует! Попробуйте снова: ");
                }
            } while (!isExist);

            Path = newPath;
        }

        public Task? GetTask()
        {
            string[] lines = File.ReadAllLines(Path, Encoding.UTF8);
            if (lines.Length == 0)
            {
                MessageAssistant.RedMessage("Файл пуст!");
                return null;
            }

            PrintTasks();

            Console.WriteLine("Выберите номер задачи: ");
            int lineIndex = Validator.GetIntInRange(1, lines.Length) - 1; // Из номера задачи вычитаем 1, чтобы узнать индекс задачи. 

            return Validator.GetTask(lines[lineIndex]);
        }

        public List<Task>? GetTasks()
        {
            string[] lines = File.ReadAllLines(Path, Encoding.UTF8);
            if (lines.Length == 0)
            {
                MessageAssistant.RedMessage("Файл пуст!");
                return null;
            }

            PrintTasks();

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
            PrintTasks();
        }

        public void WriteTask(Task task)
        {
            using (StreamWriter streamWriter = new(Path, append: true, Encoding.UTF8))
            {
                streamWriter.WriteLine($"{task.Title} | {task.Description} | {task.TaskPriority} | {(task.IsCompleted ? "Выполнена" : "Не выполнена")}");
            }
        }

        public void WriteTasks(List<Task> tasks)
        {
            using (StreamWriter streamWriter = new(Path, append: true, Encoding.UTF8))
            {
                foreach (Task task in tasks)
                {
                    streamWriter.WriteLine($"{task.Title} | {task.Description} | {task.TaskPriority} | {(task.IsCompleted ? "Выполнена" : "Не выполнена")}");
                }
            }
        }
    }
}