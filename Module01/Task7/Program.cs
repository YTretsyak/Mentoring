using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task7
{
  class Program
  {
    static void Main(string[] args)
    {
      var tcs = new TaskCompletionSource<int>();
      // a. Continuation task should be executed regardless of the result of the parent task.
      var task1 = Task.Factory.StartNew(() =>
      {
        Console.WriteLine("Task1 main method");
        tcs.SetResult(10);
        return tcs.Task;
      });

      task1.ContinueWith(
        ContiniusTask,
        TaskContinuationOptions.OnlyOnRanToCompletion);
      task1.Wait();

      // b. Continuation task should be executed when the parent task finished without success
      var task2 = Task.Run(() =>
      {
        Console.WriteLine("Task2 main method");
        throw new Exception("Task2 Exception");
      });

      var task2Cont = task2.ContinueWith(ContiniusTask,
        CancellationToken.None,
        TaskContinuationOptions.OnlyOnFaulted,
        TaskScheduler.Current);

      task2Cont.Wait();

      // c. Continuation task should be executed when the parent task would be 
      // finished with fail and parent task thread should be reused for continuation
      var task3 = Task.Factory.StartNew(() =>
      {
        Console.WriteLine("Task3 main method");
        throw new Exception("Task3 Exception");
      });

      var task3Con= task3.ContinueWith(
       ContiniusTask,
       CancellationToken.None,
       TaskContinuationOptions.ExecuteSynchronously,
       TaskScheduler.Default);

      task3Con.Wait();

      // d. d.	Continuation task should be executed outside of the thread pool 
      // when the parent task would be cancelled
      var cts = new CancellationTokenSource();

      var task4 = new Task(
        () =>
          {
            Console.WriteLine("Task4 main method");
            cts.Token.ThrowIfCancellationRequested();
          },
        cts.Token
        );

      var x = task4.ContinueWith(
        ContiniusTask,
        CancellationToken.None,
        TaskContinuationOptions.LongRunning,
        TaskScheduler.Current);

      task4.Start();
      cts.Cancel();

      Console.ReadKey();
    }

        static void ContiniusTask(Task t)
        {
          if (t.IsCanceled)
          {
            Console.WriteLine("Canceled");
          }
          else if (t.IsFaulted)
          {
            Console.WriteLine("Faulted");
            var ex = t.Exception.Flatten().InnerExceptions;
            foreach (var exception in ex)
            {
              Console.WriteLine(exception.Message);
            }

          }
          else
          {
            Console.WriteLine("Done");
          }
        }
    }
}
