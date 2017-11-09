using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task6
{
    class Program
    {
        private static BlockingCollection<int> collection;

        static void Main(string[] args)
        {
            collection = new BlockingCollection<int>(10);

            Task.Run(() =>
            {
                int i = 10;
                Random rnd = new Random();
                while (i > 0)
                {
                    collection.Add(rnd.Next(1000));
                    i--;
                }

                collection.CompleteAdding();
            });

            Task.Run(() =>
            {
                while (!collection.IsCompleted)
                {
                    int data = 0;
                    try
                    {
                        data = collection.Take();
                    }
                    catch (InvalidOperationException)
                    {
                        Console.WriteLine("Error");
                    }
                    if (data > 0)
                    {
                        Console.WriteLine(data.ToString());
                    }
                }
            }).ContinueWith((t) =>
            {
                Console.WriteLine("End...");
            });

            Console.ReadKey();
        }


    }
}
