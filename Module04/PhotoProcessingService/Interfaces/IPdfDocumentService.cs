using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoProcessingService.Interfaces
{
  public interface IPdfDocumentService
  {
    void AddImage(string fileName);
    void Save(string fileName);
  }
}
