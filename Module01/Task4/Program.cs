using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            StartProcess(10);
            Console.WriteLine("End");
            Console.ReadKey();
        }

        static void StartProcess(int numberOfThreads)
        {
            Thread thread = new Thread(() =>
            {
                Console.WriteLine(numberOfThreads);
                numberOfThreads--;
                if (numberOfThreads > 0)
                {
                    StartProcess(numberOfThreads);
                }
            });
            thread.Start();
            thread.Join();
        }
    }
}
