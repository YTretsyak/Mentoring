using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8
{
  using System.Threading;

  class Program
  {
    public static CancellationTokenSource cts = new CancellationTokenSource();
    static void Main(string[] args)
    {
      Console.WriteLine("Write a number: Write 'End' for exit ");
      Run();
    }

    public static void Run()
    {
      while (true)
      {
        var text = Console.ReadLine();
        int n = 0;
        while (int.TryParse(text, out n))
        {
          cts.Cancel();
          var cancelToken = new CancellationTokenSource().Token;
          getText(n, cancelToken);
          text = Console.ReadLine();
        }
        if (text == "End")
        {
          cts.Cancel();
          cts.Dispose();
        }
        else
        {
          Console.WriteLine("Write int number please: ");
          continue;
        }
        break;
      }
    }

    public static async Task<string> CountNumbersAsync(int number, CancellationToken token)
    {
      try
      {
        return await Task<string>.Factory.StartNew(() => GetSumOfElements(number, token), token);
      }
      catch (Exception ex)
      {
        return "123";
      }

    }

    public static string GetSumOfElements(decimal number, CancellationToken token)
    {
      decimal sum;

      if (number < 0)
      {
        sum = ((1 + Math.Abs(number)) * Math.Abs(number) / 2 - 1) * -1;
      }
      else if (number == 0)
      {
        sum = 1;
      }
      else
      {
        sum = (1 + number) * number / 2;
      }
      token.ThrowIfCancellationRequested();
      //Thread.Sleep(1000);

      return sum.ToString();
    }

    private static Action<int, CancellationToken> getText = async (number, token) =>
      {
        var sum = await CountNumbersAsync(number, token);
        Console.WriteLine(sum);
      };
  }
}
