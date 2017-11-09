using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Enter a value :");
      int value = 0;
      if (int.TryParse(Console.ReadLine(), out value))
      {
        DisplayResultsAsync(value);

        Console.WriteLine("End");
        Console.ReadLine();
      }
      else
      {
        Console.WriteLine("Incorrect number");
      }

    }

    static void DisplayResultsAsync(int value)
    {
      Task[] tasks = new Task[100];
      for (int number = 0; number < 100; number++)
      {
        tasks[number] = Task.Factory.StartNew(() => TestMethod(value));
      }

      Task.WhenAll(tasks);
    }

    static void TestMethod(int value)
    {
      for (int number = 1; number <= value; number++)
      {
        Console.WriteLine($"Task {Task.CurrentId} – {number} ");
      }
    }
  }
}
