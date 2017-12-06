using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Xml.Validator
{
  class XmlValidator
  {
    public bool Validate(string targetNamespace, string schemaPath, string filePath)
    {
      var validationErrors = new List<string>();

      var schemas = new XmlSchemaSet();
      schemas.Add(targetNamespace, XmlReader.Create(new FileStream(schemaPath, FileMode.Open, FileAccess.Read)));

      var document = XDocument.Load(new FileStream(filePath, FileMode.Open));

      document.Validate(schemas, (o, e) => { validationErrors.Add(e.Message); });

      return validationErrors.Count == 0;
    }
  }
}
