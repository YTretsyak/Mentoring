using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
  class Program
  {
    static void Main(string[] args)
    {
      // TODO:This is incorrect implemenetation without ContiniousWith mehtod
      RunTasks();
      Console.ReadKey();
    }

    static async Task RunTasks()
    {
      var rndIntMass = await generateArray();

      await multiplyArray(rndIntMass);
      await SortArrayByAscending(rndIntMass);
      await GetAverage(rndIntMass);

      Console.ReadKey();
    }

    static Task<int[]> generateArray()
    {
      return Task.Run(
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

    static Task multiplyArray(int[] array)
    {
      return Task.Run(
          () =>
          {
            Random rnd = new Random();

            for (int i = 0; i < array.Length; i++)
            {
              array[i] = array[i] * rnd.Next(1, 10);
              Console.WriteLine($"new {i} element in the array has: {array[i]} value");
            }
          });
    }

    static Task SortArrayByAscending(int[] array)
    {
      return Task.Run(
          () =>
          {
            Array.Sort(array);
            Console.WriteLine("Sorted array: ");
            foreach (var item in array)
            {
              Console.Write($"{item}, ");
            }
          });
    }

    static Task<int> GetAverage(int[] array)
    {
      return Task.Run(
          () =>
          {
            int result = 0;
            foreach (var item in array)
            {
              result += item;
            }
            Console.WriteLine();
            Console.WriteLine($"Average result is {result}");
            return result;
          });
    }
  }
}
