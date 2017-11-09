using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task5
{
    class Program
    {
        private static Semaphore sem;

        static void Main(string[] args)
        {
            sem = new Semaphore(1,10);
            StartProcess(10);
            Console.WriteLine("End...");
            Console.ReadKey();
        }


        static void StartProcess(int numberOfThreads)
        {
            sem.WaitOne();
            if (numberOfThreads > 0)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(TestMethod), numberOfThreads--);
                StartProcess(numberOfThreads);
            }
        }

        static void TestMethod(Object obj)
        {
            //sem.WaitOne();
            int number = (int)obj;
            Console.WriteLine(number);
            sem.Release();
        }
    }
}
