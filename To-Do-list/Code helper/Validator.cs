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

        public static bool IsTask(string task)
        {
            if (task.Split('|').Length == 3)
            {
                return true;
            }

            return false;
        }

        public static int GetInt()
        {
            bool isCorrect;
            int number;

            do
            {
                isCorrect = int.TryParse(Console.ReadLine(), out number);
                if (isCorrect)
                {
                    MessageAssistant.RedMessage("Неверный формат числа! Попробуйте ещё раз: ");
                }
            } while (isCorrect);
            
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
    }
}