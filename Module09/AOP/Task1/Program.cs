using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1.AOP_classes;
using Task1.Classes;

namespace Task1
{
  class Program
  {
    static void Main(string[] args)
    {
      MyAspect.SetPathToLoger("d:\\textwriter.txt"); // set log file
      var command = MyAspect.Apply<TestWriter>();
      command.Write();
      command.WriterWithParams("Hello");

      Console.ReadKey();
    }
  }
}
