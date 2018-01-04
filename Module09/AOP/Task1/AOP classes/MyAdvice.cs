using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Castle.DynamicProxy;
using Newtonsoft.Json;

namespace Task1.AOP_classes
{
  class MyAdvice:IInterceptor
  {
    public readonly TextWriter Output;

    public MyAdvice(TextWriter output)
    {
      Output = output;
    }

    public void Intercept(IInvocation invocation)
    {
      var json = ParamsToJson(invocation);
      Output.WriteLine("Method {0} with parameters {1}.",invocation.Method.Name, json);
      Output.Flush();
      invocation.Proceed();
      Output.WriteLine("Done: result was {0}.", invocation.ReturnValue);
      Output.Flush();
    }

    private string ParamsToJson(IInvocation invocation)
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
