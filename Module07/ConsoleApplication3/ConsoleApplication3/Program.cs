using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using ConsoleApplication3.Classes;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Checking for free key...wait please");
            Thread.Sleep(2000);

            var keyGen = new KeyGenerator();
            var key = keyGen.GenerateNewKey();

            Console.WriteLine(key);
            Console.ReadKey();
        }
    }
}
