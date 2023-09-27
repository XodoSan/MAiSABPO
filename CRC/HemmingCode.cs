using System.Text;

namespace CRC
{
    public static class HemmingCode
    {
        public static StringBuilder GetHemmingCode(StringBuilder message)
        {
            int counter = 0;
            int pow = 0;
            int position = 0;
            int nextPosition = 0;
            StringBuilder hemmingMessage = new StringBuilder("");
            while (counter < message.Length)// Составление хемминг-сообщения с пробелами
            {
                position = Convert.ToInt32(Math.Pow(2, pow));
                nextPosition = Convert.ToInt32(Math.Pow(2, pow + 1));
                pow++;

                hemmingMessage.Append(' ');
                for (int i = 0; i < nextPosition - position - 1; i++)
                {
                    if (counter >= message.Length)
                        break;

                    hemmingMessage.Append(message[counter]);
                    counter++;
                }
            }

            counter = 0;
            pow = 0;
            position = 0;
            while (hemmingMessage.ToString().Any(x => x == ' '))//Заполнение хемминг-сообщения суммами
            {
                position = Convert.ToInt32(Math.Pow(2, pow));
                pow++;
                string sum = "";

                sum = GetCurrentSum(position, hemmingMessage.ToString());

                if (sum.Where(x => x == '1').Count() % 2 == 0)
                    hemmingMessage[position - 1] = '0';
                else
                    hemmingMessage[position - 1] = '1';
            }

            return hemmingMessage;
        }

        //Возвращает позиции в сообщении, которые не совпадают с позициями в исходном сообщении
        public static List<int> GetFailedPositions(string hemmingMessage)
        {
            int pow = 0;
            int position = 0;
            List<int> failedPositions = new();// Позиции, символы которых разнятся
            while (true)
            {
                position = Convert.ToInt32(Math.Pow(2, pow));
                pow++;
                string sum;

                if (position >= hemmingMessage.Length)
                    break;

                sum = GetCurrentSum(position, hemmingMessage.ToString());

                int numSum = sum.Where(x => x == '1').Count();
                int numValue = Int32.Parse(hemmingMessage[position - 1].ToString());
                if (numValue != (numSum % 2))
                {
                    failedPositions.Add(position);
                }
            }

            return failedPositions;
        }

        private static string GetCurrentSum(int position, string hemmingMessage)
        {
            bool isTrue = true;
            string sum = "";

            int counter = position;
            while (isTrue)
            {
                for (int i = 0; i < position; i++)
                {
                    if (counter > hemmingMessage.Length)
                    {
                        isTrue = false;
                        break;
                    }

                    if (counter != position)
                        sum += hemmingMessage[counter - 1];
                    counter++;
                }

                counter += position;
            }

            return sum;
        }
    }
}