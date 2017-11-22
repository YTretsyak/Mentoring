using System;
using System.Diagnostics;
using System.IO;
using Topshelf;

namespace PhotoProcessingService
{
  class Program
  {
    private static string currentDir = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
    private static readonly string InputFolder = Path.Combine(currentDir,"Input");
    private static readonly string OutputFolder = Path.Combine(currentDir, "Output");
    private static readonly string CorruptedFilesFolder = Path.Combine(currentDir, "Corrupted");
    private static readonly string ServiceName = "Photo Processing Service";

    static void Main(string[] args)
    {
      // Запускаем сервис в режиме RunAsLocalSystem.
      // Перед запуском сервиса настраиваем его конфигурацию

      HostFactory.Run(configurator =>
      {
        configurator.Service(() => new PhotoProcessingService(InputFolder, OutputFolder, CorruptedFilesFolder));

        configurator.SetServiceName(ServiceName);
        configurator.SetDisplayName(ServiceName);
        configurator.SetDescription(ServiceName);

        configurator.StartAutomaticallyDelayed();
        configurator.RunAsLocalSystem();
      });

      Console.Read();
    }
  }
}
