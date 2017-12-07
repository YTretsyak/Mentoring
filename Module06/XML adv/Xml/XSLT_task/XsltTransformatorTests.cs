using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XSLT_task
{
  using System.IO;

  [TestClass]
  public class XsltTransformatorTests
  {
    [TestMethod]
    public void ShouldCreateNewXmlFileTest()
    {
      var xslt = @"../../Files/Task2_xslt.xslt";
      var xml = @"../../Files/Task2.xml";

      var transformator = new Transformator.Transformator();

      transformator.Transform(xslt, new FileStream(xml, FileMode.Open), new FileStream("result.xml",FileMode.Create));
    }

    [TestMethod]
    public void ShouldCreateNewHtmlFileTest()
    {
      var xslt = @"../../Files/Task1_xslt.xslt";
      var xml = @"../../Files/Task2.xml";

      var transformator = new Transformator.Transformator();

      transformator.Transform(xslt, new FileStream(xml, FileMode.Open), new FileStream("result.html", FileMode.Create));
    }
  }
}
