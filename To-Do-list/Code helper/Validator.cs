namespace To_Do_list.Code_helper
{
    public static class Validator
    {
        public static string GetString()
        {
            bool isEmpty;
            string line;

            do
            {
                line = Console.ReadLine()!.Trim();
                isEmpty = string.IsNullOrEmpty(line);
                if (isEmpty)
                {
                    MessageAssistant.RedMessage("Неверный формат строки! Попробуйте ещё раз: ");
                }
            } while (isEmpty);

            return line;
        }

        public static int GetInt()
        {
            bool isCorrect;
            int number;

            do
            {
                isCorrect = int.TryParse(Console.ReadLine(), out number);
                if (!isCorrect)
                {
                    MessageAssistant.RedMessage("Неверный формат числа! Попробуйте ещё раз: ");
                }
            } while (!isCorrect);

            return number;
        }

        public static int GetIntInRange(int minimumNumber, int maximumNumber)
        {
            bool isCorrect;
            int number;

            do
            {
                number = GetInt();
                isCorrect = number >= minimumNumber && number <= maximumNumber;
                if (!isCorrect)
                {
                    MessageAssistant.RedMessage($"Число должно входить в диапазон: {minimumNumber}-{maximumNumber}! Попробуйте ещё раз: ");
                }
            } while (!isCorrect);

            return number;
        }

        public static Task? GetTask(string line)
        {
            if (!IsTask(line))
            {
                MessageAssistant.RedMessage("Строка из файла имеет некорректный формат!");
                return null;
            }

            string[] parts = line.Trim().Split('|');
            return new Task(parts[0], parts[1], (TaskPriority)Enum.Parse(typeof(TaskPriority), parts[2], ignoreCase: true), parts[3].Equals("да", StringComparison.CurrentCultureIgnoreCase));
        }

        private static bool IsTask(string line) => line.Split('|').Length == 4;
    }
}