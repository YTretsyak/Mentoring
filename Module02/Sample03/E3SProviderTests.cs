﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sample03.E3SClient.Entities;
using Sample03.E3SClient;
using System.Configuration;
using System.Linq;

namespace Sample03
{
  [TestClass]
  public class E3SProviderTests
  {
    [TestMethod]
    public void WithoutProvider1()
    {
      var client = new E3SQueryClient(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);
      var res = client.SearchFTS<EmployeeEntity>("(EPRUIZHW0249):workstation", 0, 1);

      foreach (var emp in res)
      {
        Console.WriteLine("{0} {1}", emp.nativename, emp.startworkdate);
      }
    }

    [TestMethod]
    public void WithoutProvider()
    {
      var client = new E3SQueryClient(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);
      var res = client.SearchFTS<EmployeeEntity>("workstation:(EPRUIZHW0249)", 0, 1);

      foreach (var emp in res)
      {
        Console.WriteLine("{0} {1}", emp.nativename, emp.startworkdate);
      }
    }

    [TestMethod]
    public void WithoutProviderNonGeneric()
    {
      var client = new E3SQueryClient(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);
      var res = client.SearchFTS(typeof(EmployeeEntity), "workstation:(EPRUIZHW0249)", 0, 10);

      foreach (var emp in res.OfType<EmployeeEntity>())
      {
        Console.WriteLine("{0} {1}", emp.nativename, emp.startworkdate);
      }
    }

    [TestMethod]
    public void WithProvider()
    {
      var employees = new E3SEntitySet<EmployeeEntity>(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);

      foreach (var emp in employees.Where(e => e.workstation == "EPRUIZHW0249"))
      {
        Console.WriteLine("{0} {1}", emp.nativename, emp.startworkdate);
      }
    }

    [TestMethod]
    public void StartsWithTest()
    {
      var employees = new E3SEntitySet<EmployeeEntity>(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);

      foreach (var emp in employees.Where(e => e.workstation.StartsWith("EPRUIZHW0249")))
      {
        Console.WriteLine("{0} {1}", emp.nativename, emp.startworkdate);
      }
    }

    [TestMethod]
    public void EndsWithTest()
    {
      var employees = new E3SEntitySet<EmployeeEntity>(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);

      foreach (var emp in employees.Where(e => e.workstation.EndsWith("IZHW0060")))
      {
        Console.WriteLine("{0} {1}", emp.nativename, emp.startworkdate);
      }
    }

    [TestMethod]
    public void ContainsTest()
    {
      var employees = new E3SEntitySet<EmployeeEntity>(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);

      foreach (var emp in employees.Where(e => e.workstation.Contains("IZHW006")))
      {
        Console.WriteLine("{0} {1}", emp.nativename, emp.startworkdate);
      }
    }

    [TestMethod]
    public void AndTest()
    {
      var employees = new E3SEntitySet<EmployeeEntity>(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);

      foreach (var emp in employees.Where(e => e.workstation.Contains("EPBYMINW4398") && e.project.StartsWith("P")))
      {
        Console.WriteLine("{0} {1}", emp.nativename, emp.startworkdate);
      }
    }
  }
}
