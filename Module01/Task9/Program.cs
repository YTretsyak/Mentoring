using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9
{
  using System.Threading;

  public class Program
  {
    static void Main(string[] args)
    {
      Action<string, CancellationToken> getCount = async (input, token) =>
      {
        var result = await StartCountAsync(input, token);
        Console.WriteLine(result);
      };

      Console.WriteLine("Starting. Please enter number. For exit tape E");
      var UserInput = Console.ReadLine();
      CancellationTokenSource cts = new CancellationTokenSource();
      while (!UserInput.ToUpper().StartsWith("E"))
      {
        getCount(UserInput, cts.Token);
        UserInput = Console.ReadLine();
        cts.Cancel();
        cts.Dispose();
        cts = new CancellationTokenSource();
      }
    }

    public static async Task<string> StartCountAsync(string userNumberText, CancellationToken token)
    {
      try
      {
        return await Task<string>.Factory.StartNew(() => GetCounter(userNumberText, token), token);
      }
      catch
      {
        return "Canceled";
      }
    }

    public static string GetCounter(string userNumberText, CancellationToken token)
    {
      //var result = new Counter();
      long userNumberlong = 0;
      long.TryParse(userNumberText, out userNumberlong);

      return string.Concat("userNumber=", userNumberlong, " sumUserNumber=", Count(userNumberlong, token));
    }

    public static long Count(long maxNumber, CancellationToken token)
    {
      long result = 0;

      for (int i = 0; i <= maxNumber; i++)
      {
        token.ThrowIfCancellationRequested();
        var br = result.ToString() + i.ToString();
        result += i;
      }
      var br2 = result.ToString() + maxNumber.ToString();
      return result;
    }
  }
}
