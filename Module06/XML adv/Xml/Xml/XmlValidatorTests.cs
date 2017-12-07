using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xml.Validator;

namespace Xml
{
  [TestClass]
  public class XmlValidatorTests
  {
    [TestMethod]
    public void ShouldValidateXmlFile()
    {
      var validator = new XmlValidator();
      var pathToXml = @"../../Files/Task1.xml";
      var pathToXsd = @"../../Files/Task1_Schema.xsd";

      var valid = validator.Validate("http://tempuri.org/Task1_Schema.xsd", pathToXsd, pathToXml);

      Console.WriteLine(valid);

      Assert.AreEqual(true, valid);
    }

    [TestMethod]
    public void ShouldValidateXmlFile2()
    {
      var validator = new XmlValidator();
      var pathToXml = @"../../Files/Task1_non_valid.xml";
      var pathToXsd = @"../../Files/Task1_Schema.xsd";

      var valid = validator.Validate("http://tempuri.org/Task1_Schema.xsd", pathToXsd, pathToXml);

      Console.WriteLine(valid);

      Assert.AreEqual(false, valid);
    }
  }
}
