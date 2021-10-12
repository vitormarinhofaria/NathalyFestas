using BlazorTemplater;
using NathalyFestasWeb.Models;
using NathalyFestasWeb.Shared;
using SelectPdf;
using System.IO;
using PreMailer;

namespace NathalyFestasWeb.Services
{
    public class PdfManager
    {
        public static byte[] GetPedidoPdf(Pedido pedido)
        {
            string html = new ComponentRenderer<PedidoPdf>().Set(c => c.Pedido, pedido).Render();
            string bootstrapCss = File.ReadAllText("wwwroot/css/bootstrap/bootstrap.min.css");
            var preMailer = PreMailer.Net.PreMailer.MoveCssInline(html, css: bootstrapCss);
            HtmlToPdf converter = new();
            PdfDocument doc = converter.ConvertHtmlString(preMailer.Html, "/");
            string fileName = $"{pedido.Id}.pdf";
            doc.Save(fileName);
            byte[] pdfBytes = File.ReadAllBytes(fileName);
            return pdfBytes;
        }
    }
}
