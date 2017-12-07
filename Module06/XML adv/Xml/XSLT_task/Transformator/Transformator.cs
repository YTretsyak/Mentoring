using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace XSLT_task.Transformator
{
  public class Transformator
  {
    public void Transform(string xsltPath, Stream input, Stream output)
    {
      var xsltSettings = new XsltSettings() { EnableScript = true };

      var xslt = new XslCompiledTransform();
      xslt.Load(XmlReader.Create(xsltPath), xsltSettings, null);

      var xmlDocument = new XPathDocument(input);

      var xmlWriterSettings = new XmlWriterSettings()
      {
        OmitXmlDeclaration = false,
        Indent = true,
        ConformanceLevel = ConformanceLevel.Fragment,
        CloseOutput = false
      };

      var writer = XmlWriter.Create(output, xmlWriterSettings);
      xslt.Transform(xmlDocument, new XsltArgumentList(), writer);
    }


  }
}
