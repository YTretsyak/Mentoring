using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MigraDoc.Rendering;
using PdfSharp.Pdf;

namespace MessageQueueServer
{
  class Program
  {
    private static MessageQueue _queue;
    private static string currentDir = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
    private static readonly string OutputFolder = Path.Combine(currentDir, "Output");

    static void Main(string[] args)
    {
      Console.WriteLine("Starting the server...");
      Console.WriteLine("Connecting to message queuq...");

      if (MessageQueue.Exists(@".\private$\MyObjectQueue"))
      {
        _queue = new MessageQueue(@".\private$\MyObjectQueue");
        _queue.Formatter = new BinaryMessageFormatter();
      }
      else
      {
        Console.WriteLine("Queue doesn't exist");
      }

      if (_queue != null)
      {
        Console.WriteLine("Write 1 if you want reseave a message from queue");
        Console.WriteLine("Write 2 for exit");

        for (; ; )
        {
          int value;
          if (int.TryParse(Console.ReadLine(), out value))
          {
            if (value == 1)
            {
              try
              {
                var message = _queue.Receive();
                if (message != null)
                {
                  var bDocument = (byte[]) message.Body;
                  var uniqueFileName = string.Format(@"{0}-file.pdf", Guid.NewGuid());
                  var newDoc = OutputFolder + "/" + uniqueFileName;

                  if (!Directory.Exists(OutputFolder))
                  {
                    Directory.CreateDirectory(OutputFolder);
                  }

                  File.WriteAllBytes(newDoc, bDocument);
                }
              }
              catch (MessageQueueException)
              {
                Console.WriteLine("Something wrong.Try to get a file later");
                Thread.Sleep(5000);
              }
            }
            else if (value == 2)
            {
              break;
            }
          }
        }

        Console.WriteLine("Server has been stoped.");
      }
    }
  }
}
