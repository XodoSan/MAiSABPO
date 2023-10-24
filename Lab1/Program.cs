using System.Numerics;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Введите номер задания: ");
        string numOfTask = Console.ReadLine();
        switch (numOfTask)
        {
            case "1":
                Program1();
                break;
            case "2":
                Program2();
                break;
            case "3":
                Program3();
                break;
            case "4":
                Program4();
                break;
            case "5":
                Program5();
                break;
            default:
                break;
        }
    }
    static void Program1()
    {
        Console.Write("Введите формат: ");
        string format = Console.ReadLine();
        Console.Write("Введите путь к файлу: ");
        string address = Console.ReadLine();

        switch (format)
        {
            case "hex8":
                using (FileStream fs = new FileStream(address, FileMode.Open))
                {
                    int bytesRead;
                    byte[] buffer = new byte[8];
                    while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        for (int i = 0; i < bytesRead; i++)
                        {
                            Console.Write(buffer[i].ToString("X2") + " ");
                        }
                    }
                }
                break;

            case "dec8":
                using (FileStream fs = new FileStream(address, FileMode.Open))
                {
                    int bytesRead;
                    byte[] buffer = new byte[8];
                    while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        for (int i = 0; i < bytesRead; i++)
                        {
                            Console.Write(buffer[i].ToString() + " ");
                        }
                    }
                }
                break;

            case "hex16":
                using (FileStream fs = new FileStream(address, FileMode.Open))
                {
                    int bytesRead;
                    byte[] buffer = new byte[16];
                    while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        for (int i = 0; i < bytesRead; i += 2)
                        {
                            ushort value = BitConverter.ToUInt16(buffer, i);
                            Console.Write(value.ToString("X4") + " ");
                        }
                    }
                }
                break;

            case "dec16":
                using (FileStream fs = new FileStream(address, FileMode.Open))
                {
                    int bytesRead;
                    byte[] buffer = new byte[2];
                    while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        short value = BitConverter.ToInt16(buffer, 0);
                        Console.Write(value.ToString() + " ");
                    }
                }
                break;

            case "hex32":
                using (FileStream fs = new FileStream(address, FileMode.Open))
                {
                    int bytesRead;
                    byte[] buffer = new byte[4];

                    while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        int value = BitConverter.ToInt32(buffer, 0);
                        Console.Write(value.ToString("X8") + " ");
                    }
                }
                break;
        }
    }
    static void Program2()
    {
        Console.Write("Введите команду: ");
        string command = Console.ReadLine();
        Console.Write("Введите число1: ");
        string number1 = Console.ReadLine();
        Console.Write("Введите число2: ");
        string number2 = Console.ReadLine();
        string binaryString1 = DecimalToBinary(Convert.ToUInt64(number1));
        string binaryString2 = DecimalToBinary(Convert.ToUInt64(number2));
        string binaryResult;
        int firstStringLength;
        int secondStringLength;
        int _maxLength;
        string result = "";
        switch (command)
        {
            case "xor":

                Console.WriteLine(binaryString1);
                Console.WriteLine(binaryString2);
                int maxLength = Math.Max(binaryString1.Length, binaryString2.Length);
                binaryString1 = binaryString1.PadLeft(maxLength, '0');
                binaryString2 = binaryString2.PadLeft(maxLength, '0');

                result = "";
                for (int i = 0; i < maxLength; i++)
                {
                    result += binaryString1[i] == binaryString2[i] ? "0" : "1";
                }
                Console.WriteLine(result);
                break;
            case "and":
                Console.WriteLine(binaryString1);
                Console.WriteLine(binaryString2);
                binaryResult = "";
                firstStringLength = binaryString1.Length;
                secondStringLength = binaryString2.Length;
                _maxLength = Math.Max(firstStringLength, secondStringLength);
                binaryString1 = binaryString1.PadLeft(_maxLength, '0');
                binaryString2 = binaryString2.PadLeft(_maxLength, '0');
                for (int i = 0; i < _maxLength; i++)
                {
                    if (binaryString1[i].ToString() == "1" && binaryString2[i].ToString() == "1")
                    {
                        binaryResult += "1";
                    }
                    else
                    {
                        binaryResult += "0";
                    }
                }
                Console.WriteLine(binaryResult);
                break;
            case "or":
                Console.WriteLine(binaryString1);
                Console.WriteLine(binaryString2);
                binaryResult = "";
                firstStringLength = binaryString1.Length;
                secondStringLength = binaryString2.Length;
                _maxLength = Math.Max(firstStringLength, secondStringLength);
                binaryString1 = binaryString1.PadLeft(_maxLength, '0');
                binaryString2 = binaryString2.PadLeft(_maxLength, '0');
                for (int i = 0; i < _maxLength; i++)
                {
                    if (binaryString1[i].ToString() == "1" || binaryString2[i].ToString() == "1")
                    {
                        binaryResult += "1";
                    }
                    else
                    {
                        binaryResult += "0";
                    }
                }
                Console.WriteLine(binaryResult);
                break;
            case "set1":
                Console.WriteLine(binaryString1);
                char[] r = binaryString1.ToCharArray();
                r[Convert.ToInt32(number2) - 1] = '1';
                Console.WriteLine(r);
                break;
            case "set0":
                Console.WriteLine(binaryString1);
                char[] r1 = binaryString1.ToCharArray();
                r1[Convert.ToInt32(number2) - 1] = '0';
                Console.WriteLine(r1);
                break;
            case "shl":
                Console.WriteLine(binaryString2);
                string paddedString1 = binaryString2.PadRight(binaryString2.Length + binaryString1.Length, '0');
                Console.WriteLine(paddedString1.Substring(binaryString1.Length));
                break;
            case "shr":
                Console.WriteLine(binaryString2);
                string paddedString = binaryString2.PadLeft(binaryString2.Length + binaryString1.Length, '0');
                string trimmedString = paddedString.Substring(0, paddedString.Length - binaryString1.Length);
                Console.WriteLine(trimmedString);
                break;
            case "rol":
                Console.WriteLine(binaryString2);
                Console.WriteLine(binaryString2.Substring(binaryString1.Length) + binaryString2.Substring(0, binaryString1.Length));
                break;
            case "ror":
                Console.WriteLine(binaryString2);
                string last4Characters = binaryString2.Substring(binaryString2.Length - binaryString1.Length);
                string resultString = last4Characters + binaryString2.Substring(0, binaryString2.Length - binaryString1.Length);
                Console.WriteLine(resultString);
                break;
            case "mix":
                binaryString2 = binaryString2.PadLeft(8, '0');

                for (int i = 0; i < number1.Length; i++)
                {
                    int e = Convert.ToInt32(number1[i].ToString());

                    result += binaryString2[e];
                }
                Console.WriteLine(result);
                break;
            default:
                break;
        }
    }
    static void Program3()
    {
        Console.Write("Введите a ");
        int a = int.Parse(Console.ReadLine().ToString());
        Console.Write("Введите b ");
        int b = int.Parse(Console.ReadLine().ToString());
        Console.Write("Введите M ");
        int M = int.Parse(Console.ReadLine().ToString());
        Console.WriteLine("Базовые арифметические операции в поле модуля");
        Console.WriteLine("a+b mod M " + (a + b) % M);
        Console.WriteLine("a-b mod M " + ((a - b) + M) % M);
        Console.WriteLine("b-a mod M " + (b - a) % M);
        Console.WriteLine("a*b mod M " + (a * b) % M);
        Console.WriteLine("a/b mod M " + (a / b) % M);
        Console.WriteLine("b/a mod M " + (b / a) % M);
        Console.WriteLine("a^b mod M " + ModPow(a, b, M));
        Console.WriteLine("a^-1 mod M " + ModInverse(a, M));
        Console.WriteLine("b^-1 mod M " + ModInverse(b, M));
        Console.WriteLine("Базовые арифметические операции в поле полинома GF(2,n)");
        Console.WriteLine("a+b mod M " + (a ^ b) % M);
        Console.WriteLine("a-b mod M " + (a ^ b) % M);
        Console.WriteLine("a*b mod M " + Multiply(a, b, M));
        Console.WriteLine("2^-1 mod M " + ModInverse(2, M));

    }
    static void Program4()
    {
        Console.Write("Введите N: ");
        int N = Convert.ToInt32(Console.ReadLine().ToString());
        List<int> a = new List<int>();
        for (int i = 2; i <= N; i++)
        {
            a.Add(i);
        }
        for (int i = 0; i < a.Count; i++)
        {
            for (int j = 0; j < a.Count; j++)
            {

                if (a[j] != a[i])
                {
                    if (a[j] % a[i] == 0)
                    {
                        a.Remove(a[j]);
                    }

                }
            }

        }

        foreach (int i in a) Console.WriteLine(i);
        if (a.Contains(N))
        {
            Console.WriteLine(N + " простое число");
        }
        else
        {
            Console.WriteLine(N + " не простое число");
        }
    }
    static void Program5()
    {
        MyBigInteger a = new MyBigInteger("12345");
        MyBigInteger b = new MyBigInteger("54321");
        MyBigInteger mod = new MyBigInteger("1234");
        Console.WriteLine("a*b=" + a * b);
        Console.WriteLine("a+b=" + (a + b));
        Console.WriteLine("a*b mod 1234=" + (a * b) % mod);
        BigInteger firstStringLength = BigInteger.Parse("12345");
        BigInteger a2 = BigInteger.Parse("54321");
        Console.WriteLine("firstStringLength*a2=" + firstStringLength * a2);
        Console.WriteLine("firstStringLength+a2=" + (firstStringLength + a2));
        Console.WriteLine("firstStringLength*a2 mod 1234=" + ((firstStringLength * a2) % 1234));
    }
    static int Multiply(int a, int b, int m)
    {
        int result = 0;
        while (b > 0)
        {
            if ((b & 1) == 1)
            {
                result ^= a;
            }
            a <<= 1;
            if ((a & (1 << 16)) != 0)
            {
                a ^= m;
            }
            b >>= 1;
        }
        return result;
    }
    public static long ModInverse(long a, long m)
    {
        long m0 = m;
        long y = 0, x = 1;

        if (m == 1)
            return 0;
        try
        {
            while (a > 1)
            {
                long q = a / m;
                long t = m;

                m = a % m;
                a = t;
                t = y;

                y = x - q * y;
                x = t;
            }

            if (x < 0)
            {
                x += m0;
            }

            return x;
        }
        catch
        {
            return 0;
        }
    }
    static long ModPow(long a, long b, long m)
    {
        if (b == 0)
            return 1;

        long result = 1;
        a %= m;

        while (b > 0)
        {
            if ((b & 1) == 1)
                result = (result * a) % m;

            a = (a * a) % m;
            b >>= 1;
        }

        return result;
    }
    public static string DecimalToBinary(UInt64 decimalNumber)
    {
        string binaryString = "";
        do
        {
            binaryString = (decimalNumber % 2) + binaryString;
            decimalNumber /= 2;
        } while (decimalNumber > 0);

        return binaryString;
    }

}
public class MyBigInteger
{

    private string value;

    public MyBigInteger(string value)
    {
        this.value = value;
    }
    public static MyBigInteger operator +(MyBigInteger a, MyBigInteger b)
    {

        string result = Add(a.value, b.value);
        return new MyBigInteger(result);
    }

    public static MyBigInteger operator *(MyBigInteger a, MyBigInteger b)
    {

        string result = Multiply(a.value, b.value);
        return new MyBigInteger(result);
    }
    public override string ToString()
    {
        return value;
    }
    public static MyBigInteger operator %(MyBigInteger a, MyBigInteger modulus)
    {

        string result = Modulus(a.value, modulus.value);
        return new MyBigInteger(result);
    }
    private static string Add(string a, string b)
    {
        string result = "";
        int v = 0;
        for (int i = a.Length - 1; i >= 0; i--)
        {

            result = (((int.Parse(a[i].ToString())) + int.Parse(b[i].ToString()) + v) % 10) + result;
            if ((int.Parse(a[i].ToString()) + int.Parse(b[i].ToString()) + v) >= 10)
                if ((int.Parse(a[i].ToString()) + int.Parse(b[i].ToString()) + v) == 10)
                {
                    v = (int.Parse(a[i].ToString()) + int.Parse(b[i].ToString()) + v + 1) % 10;
                }
                else
                {
                    v = (int.Parse(a[i].ToString()) + int.Parse(b[i].ToString()) + v) % 10;
                }

        }
        return result;
    }
    private static string Multiply(string a, string b)
    {
        int len1 = a.Length;
        int len2 = b.Length;
        int[] product = new int[len1 + len2];

        for (int i = len1 - 1; i >= 0; i--)
        {
            for (int j = len2 - 1; j >= 0; j--)
            {
                int digit1 = a[i] - '0';
                int digit2 = b[j] - '0';
                int mul = digit1 * digit2;

                int p1 = i + j;
                int p2 = i + j + 1;

                int sum = mul + product[p2];

                product[p1] += sum / 10;
                product[p2] = sum % 10;
            }
        }

        string result = string.Join("", product).TrimStart('0');
        return string.IsNullOrEmpty(result) ? "0" : result;
    }
    private static string Modulus(string a, string modulus)
    {
        string result = a;
        string modulusString = modulus;
        string remainderString = "0";

        // Выполняем операцию взятия остатка (модуль)
        while (result.Length >= modulusString.Length)
        {
            int modLength = modulusString.Length;
            string currentSegment = result.Substring(0, modLength);

            int currentRemainder = int.Parse(remainderString + currentSegment) % int.Parse(modulusString);
            remainderString = currentRemainder.ToString();
            result = result.Substring(modLength);
        }

        return string.IsNullOrEmpty(remainderString + result) ? "0" : remainderString + result;
    }
}




//internal class Program
//{
//    private static void Main()
//    {
//        string[] args = new string[2];
//        Console.WriteLine("Enter format and file path through a space: ");
//        args = Console.ReadLine()!.Split(' ');
//        string format = args[0];
//        string filePath = args[1];
//        string number = "";

//        if (!File.Exists(filePath))
//        {
//            throw new ArgumentNullException("File does not exist");
//        }
//        number = File.ReadAllText(filePath);

//        Console.WriteLine(format + ": ");
//        switch (format)
//        {
//            case "hex8":
//                int counter = 0;
//                string result = int.Parse(number).ToString("X8");
//                while (counter < result.Length)
//                {
//                    if (counter % 2 == 0 && counter != 0)
//                    {
//                        Console.Write(' ');
//                    }
//                    Console.Write(result[counter]);
//                    counter++;
//                }
//                break;
//            case "dec8":
//                counter = 0;
//                var bytes = BitConverter.GetBytes(int.Parse(number));
//                for (int i = 0; i < bytes.Length; i++)
//                {
//                    Console.Write(bytes[i].ToString("0"));
//                    if ((i + 2) % 3 == 0 && i != 0)
//                    {
//                        Console.Write(' ');
//                    }
//                }
//                break;
//            case "hex16":
//                counter = 0;
//                result = int.Parse(number).ToString("X16");
//                while (counter < result.Length)
//                {
//                    if (counter % 4 == 0 && counter != 0)
//                    {
//                        Console.Write(' ');
//                    }
//                    Console.Write(result[counter]);
//                    counter++;
//                }
//                break;
//            case "dec16":
//                bytes = BitConverter.GetBytes(int.Parse(number));
//                for (int i = 0; i < bytes.Length; i++)
//                {
//                    Console.Write(String.Format("{0:N}", BitConverter.ToInt16(bytes, i)) + ' ');
//                }
//                break;
//            case "hex32":
//                bytes = BitConverter.GetBytes(int.Parse(number));
//                for (int i = 0; i < bytes.Length; i++)
//                {
//                    Console.Write(String.Format("{0:X}", BitConverter.ToInt32(bytes, i)) + ' ');
//                }
//                break;
//        }
//    }
//}