using CRCFUCK;
using System.Text;

namespace CRC
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("How much bit depth do you want? Print a number");// скольки разрядный алгоритм 
            string bitDepth = Console.ReadLine()!; // (какая старшая степень полинома)

            //Work with 4 bit
            //uint message = 0b_1001_1101;
            //uint polynomial = 0b_11001;

            //Work with 16 bit
            uint message = 0x9005;
            uint polynomial = 0x8005;

            //Work with 32 bit
            //uint message = 0x07C11DB7;
            //uint polynomial = 0x04C11DB7;

            string stringMessage = Convert.ToString(message, 2);// Добавление количества нулей, соответствующего старшей степени полинома
            Console.WriteLine("Start message: " + stringMessage);
            StringBuilder builderStringMessage = new StringBuilder(stringMessage);
            stringMessage = HemmingCode.GetHemmingCode(builderStringMessage).ToString(); //hemmingCode in string
            Console.WriteLine("Message after hemming code: " + stringMessage);
            for (int i = 0; i < Int32.Parse(bitDepth); i++)
            {
                stringMessage += "0";
            }

            string stringPolynomial = Convert.ToString(polynomial, 2);
            string crc = CRCCustom.ExecuteCRC(stringMessage, stringPolynomial);// Вычисление CRC

            StringBuilder crcMessage = new StringBuilder("");
            crcMessage = new StringBuilder(stringMessage.Substring(0, stringMessage.Length - crc.Length));
            crcMessage.Append(crc.ToString());
            Console.WriteLine("Message with CRC: " + crcMessage);
            if (crcMessage[0] == '0')
                crcMessage[0] = '1';
            else
                crcMessage[0] = '0'; //Если изменить основное сообщение результат должен получится ненулевым

            string result = CRCCustom.ExecuteCRC(crcMessage.ToString(), stringPolynomial);// Вычисление заново для обнаружения ошибки

            if (result.Contains('1'))
            {
                Console.WriteLine("Received message: " + crcMessage);
                Console.WriteLine("Message is currupt!");
                crcMessage.Remove(crcMessage.Length - int.Parse(bitDepth), int.Parse(bitDepth));

                List<int> failedPositions = HemmingCode.GetFailedPositions(crcMessage.ToString());// Подсчёт неверных позиций
                while (failedPositions.Count() > 0)
                {
                    int failedPosition = failedPositions.Sum() - 1;
                    if (crcMessage[failedPosition] == '0')
                        crcMessage[failedPosition] = '1';
                    else
                        crcMessage[failedPosition] = '0';

                    failedPositions = HemmingCode.GetFailedPositions(crcMessage.ToString());
                }

                Console.WriteLine("Fixed message by Hemming code: " + crcMessage);
            }
            else
            {
                Console.WriteLine("Message is successfull transferred");
            }
        }
    }
}