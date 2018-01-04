using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PostSharp.Aspects;
using PostSharp.Serialization;


namespace Task1WithPostSharp.AOP_classes
{
  [PSerializable]
  public class MyAspect : OnMethodBoundaryAspect
  {
    //private StreamWriter sw = new StreamWriter("d:\\textwriter.txt");

    public override void OnEntry(MethodExecutionArgs args)
    {
      var json = ParamsToJson(args);
      //using (sw)
      //{
      //  sw.WriteLine("Method {0} with parameters {1}.", args.Method.Name, json);
      //  sw.Flush();
      //}
      Console.WriteLine("Method {0} with parameters {1}.", args.Method.Name, json);
      base.OnEntry(args);
    }

    public override void OnExit(MethodExecutionArgs args)
    {
      //using (sw)
      //{
      //  sw.WriteLine("Method {0} with parameters was finished.", args.Method.Name);
      //  sw.Flush();
      //}
      Console.WriteLine("Method {0} was finished.", args.Method.Name);
      base.OnExit(args);
    }

    private string ParamsToJson(MethodExecutionArgs invocation)
    {
      var parameters = invocation.Method.GetParameters();
      var pars = new List<string>();
      foreach (var parameter in parameters)
      {
        pars.Add(parameter.Name);
      }

      if (pars.Count == 0)
      {
        pars.Add("Not serializable");
      }
      var json = JsonConvert.SerializeObject(pars);

      return json;
    }
  }
}
