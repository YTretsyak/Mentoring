using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Aspects;
using PostSharp.Extensibility;
using Task1WithPostSharp.AOP_classes;

namespace Task1WithPostSharp.Classes
{
  public class TestWriter
  {
    private string Word { get; }

    public TestWriter()
    {
      Word = "Empty";
    }

    public TestWriter(string word)
    {
      Word = word;
    }

    [MyAspect]
    public virtual void Write()
    {
      Console.WriteLine(Word);
    }

    [MyAspect]
    public virtual string WriterWithParams(string someString)
    {
      return someString + " test";
    }
  }
}
