using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1WithPostSharp.Classes;

namespace Task1WithPostSharp
{
  class Program
  {
    static void Main(string[] args)
    {
      var writer = new TestWriter();
      writer.Write();
      writer.WriterWithParams("test");
      Console.ReadKey();
    }
  }
}
