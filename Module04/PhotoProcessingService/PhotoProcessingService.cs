using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using PhotoProcessingService.Interfaces;
using Topshelf;

namespace PhotoProcessingService
{
  public class PhotoProcessingService : ServiceControl
  {
    private readonly string _inputFolder;
    private readonly string _outputFolder;
    private readonly string _corruptedFolder;
    private readonly List<string> _folders;
    private readonly Task _serviceTask;
    private readonly CancellationTokenSource _serviceCancellationTokenSource;
    private FileSystemWatcher _watcher;
    private bool _documentInWriteMode = false;
    private PdfDocumentService _pdfDocumentService = null;

    public PhotoProcessingService(string InputFolder, string OutputFolder, string CorruptedFolder)
    {
      _inputFolder = InputFolder;
      _outputFolder = OutputFolder;
      _corruptedFolder = CorruptedFolder;
      _folders = new List<string> {_inputFolder, _outputFolder, _corruptedFolder};
      _serviceCancellationTokenSource = new CancellationTokenSource();
      _serviceTask = new Task(() => ServiceProcedure(_serviceCancellationTokenSource.Token));

      this.SetupFolders(_folders);

      _watcher = new FileSystemWatcher(_inputFolder);
      _watcher.Created += Watcher_Created;
    }

    public bool Start(HostControl hostControl)
    {
      _serviceTask.Start();
      _watcher.EnableRaisingEvents = true;

      return true;
    }

    public bool Stop(HostControl hostControl)
    {
      _serviceCancellationTokenSource.Cancel();
      _watcher.EnableRaisingEvents = false;
      _serviceTask.Wait();

      return false;
    }

    private void ServiceProcedure(CancellationToken token)
    {
      do
      {

      }
      while (!token.IsCancellationRequested);
    }

    private bool IsValidFileName(string fileName) => Regex.IsMatch(fileName, @"^img_[0-9]{3}.(jpg|png|jpeg)$");
    private bool IsEndFile(string fileName) => Regex.IsMatch(fileName, @"^img_[0-9]{3}End.(jpg|png|jpeg)$");

    private void Watcher_Created(object sender, FileSystemEventArgs e)
    {
      if (TryOpen(e.FullPath, 3))
      {
        if (_documentInWriteMode == false)
        {
          _pdfDocumentService = new PdfDocumentService();
          _documentInWriteMode = true;
        }
        if (IsEndFile(e.Name))
        {
          _pdfDocumentService.AddImage(e.Name);

          var uniqueFileName = string.Format(@"{0}-{1}", Guid.NewGuid(), e.Name);
          var newDoc = _outputFolder + "/" + uniqueFileName;
          _pdfDocumentService.Save(newDoc); // e.Name

          _documentInWriteMode = false;
          _pdfDocumentService = null;
        }
        else if (IsValidFileName(e.Name))
        {
          _pdfDocumentService.AddImage(e.Name);
        }
      }
      else
      {
        var corruptedFileName = _corruptedFolder + "/" + e.Name;
        File.Move(e.FullPath, corruptedFileName);
      }
    }

    private bool TryOpen(string fileName, int tryCount)
    {
      for (int i = 0; i < tryCount; i++)
      {
        try
        {
          var file = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.None);
          file.Close();
          return true;
        }
        catch (IOException)
        {
          Thread.Sleep(5000);
        }
      }
      return false;
    }

    private void SetupFolders(List<string> folders)
    {
      foreach (var folder in folders)
      {
        if (!Directory.Exists(folder))
        {
          Directory.CreateDirectory(folder);
        }
      }
    }
  }
}
