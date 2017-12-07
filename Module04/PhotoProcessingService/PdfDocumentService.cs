using System.Collections.Specialized;
using System.Diagnostics;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Pdf;
using PhotoProcessingService.Interfaces;

namespace PhotoProcessingService
{
  internal class PdfDocumentService : IPdfDocumentService
  {
    private readonly Document _document;
    private readonly Section _documentSection;

    public PdfDocumentService()
    {
      _document = new Document();
      _documentSection = _document.AddSection();
    }

    public void AddImage(string fileName)
    {
      var image = _documentSection.AddImage(fileName);

      image.Height = _document.DefaultPageSetup.PageHeight;
      image.Width = _document.DefaultPageSetup.PageWidth;
      image.ScaleHeight = 0.75;
      image.ScaleWidth = 0.75;

      _documentSection.AddPageBreak();
    }

    public void Save(string fileName)
    {
      var documentRender = new PdfDocumentRenderer();

      documentRender.Document = _document;
      documentRender.RenderDocument();

      documentRender.Save(fileName);
    }

    public PdfDocument CreatePdfDocument()
    {
      var documentRender = new PdfDocumentRenderer();
      documentRender.Document = _document;
      documentRender.RenderDocument();
      return documentRender.PdfDocument;
    }
  }
}
