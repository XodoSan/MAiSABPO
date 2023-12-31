﻿using System.Diagnostics;
using static Kernel32;

namespace MAiSABPO
{
    static class Program
    {
        static void Main(string[] args)
        {
            int bytesRW = 0;
            int N;
            byte[] buffer = new byte[64 * 1024 * 1024];
            Random random = new Random((int)DateTime.Now.Ticks);
            Console.Write("Ptr=");
            string ptr = Console.ReadLine();
            UInt64 lpAddress = UInt64.Parse(ptr, System.Globalization.NumberStyles.HexNumber);// 0x0000011513ED1048;

            Process process = Process.GetProcessesByName("loop")[0];
            IntPtr processHandle = OpenProcess(PROCESS_ALL_ACCESS, false, process.Id);

            do
            {
                ReadProcessMemory((int)processHandle, lpAddress, buffer, buffer.Length, ref bytesRW);
                Console.WriteLine($"Read {bytesRW} from {lpAddress:X8}");
                for (int i = 0; i < 32; ++i)
                    Console.Write("{0:X2} ", buffer[i]);
                Console.WriteLine();

                int randomAmount = random.Next(3);
                for (int i = 0; i < randomAmount + 1; i++)
                {
                    N = random.Next(buffer.Length);
                    buffer[i] = (byte)~buffer[N];
                    Console.WriteLine($"Corrupt {N}th byte");
                }

                for (int i = 0; i < 32; ++i)
                    Console.Write("{0:X2} ", buffer[i]);
                Console.WriteLine();

                WriteProcessMemory((int)processHandle, lpAddress, buffer, buffer.Length, ref bytesRW);
                Console.WriteLine($"Wrote {bytesRW} to {lpAddress:X8}");
                Console.WriteLine("Press key to continue");
                Console.ReadKey();
            } while (true);
        }
    }
}