using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2Var2
{
  class Program
  {
    static void Main(string[] args)
    {
      RunTasks();
      Console.ReadKey();
    }

    static async Task RunTasks()
    {
      var task1 = generateArray();
      task1
        .ContinueWith<int[]>(t => multiplyArray(t.Result))
        .ContinueWith(t => SortArrayByAscending(t.Result))
        .ContinueWith(t => GetAverage(t.Result));

      Console.ReadKey();
    }

    static Task<int[]> generateArray()
    {
      return Task.Run<int[]>(
          () =>
          {
            Random rnd = new Random();
            var randomIntegers = new int[10];
            for (int i = 0; i < 10; i++)
            {
              randomIntegers[i] = rnd.Next(1000);
              Console.WriteLine($"{i} element in the array has: {randomIntegers[i]} value");
            }
            return randomIntegers;
          });
    }

    static int[] multiplyArray(int[] prevTask)
    {
      Random rnd = new Random();

      for (int i = 0; i < prevTask.Length; i++)
      {
        prevTask[i] = prevTask[i] * rnd.Next(1, 10);
        Console.WriteLine($"new {i} element in the array has: {prevTask[i]} value");
      }
      return prevTask;

    }

    static int[] SortArrayByAscending(int[] array)
    {
      Array.Sort(array);
      Console.WriteLine("Sorted array: ");
      foreach (var item in array)
      {
        Console.Write($"{item}, ");
      }

      return array;
    }

    static int GetAverage(int[] array)
    {
      int result = 0;
      foreach (var item in array)
      {
        result += item;
      }
      Console.WriteLine();
      Console.WriteLine($"Average result is {result}");

      return result;
    }
  }
}
