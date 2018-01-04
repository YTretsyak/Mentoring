using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Classes
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

    public virtual void Write()
    {
      Console.WriteLine(Word);
    }

    public virtual string WriterWithParams(string someString)
    {
      return someString + " test";
    }
  }
}
