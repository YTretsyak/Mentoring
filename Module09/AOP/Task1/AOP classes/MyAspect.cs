using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace Task1.AOP_classes
{
  static class MyAspect
  {
    private static string PathToFile { get; set; }
    public static void SetPathToLoger(string path)
    {
      PathToFile = path;
    }
    public static TClass Apply<TClass>() where TClass : class
    {
      var generator = new ProxyGenerator();
      if (PathToFile == null)
      {
        throw new Exception("Set Path to log file by using PathToFile property");
      }
      var textWrite = new StreamWriter(PathToFile);//"d:\\textwriter.txt"
      var proxy = generator.CreateClassProxy<TClass>(new MyAdvice(textWrite));
      return proxy;
    }
  }
}
