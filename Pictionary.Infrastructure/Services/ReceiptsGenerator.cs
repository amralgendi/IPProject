using System.Data;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Pictionary.Application.Orders.Interfaces;
using Pictionary.Domain.OrderModel;
using Pictionary.Domain.OrderModel.Constants;
using Pictionary.Domain.OrderModel.ValueObjects;

namespace Pictionary.Infrastructure.Services;

public class ReceiptGenerator : IReceiptGenerator
{
    public string GenerateReceipt(Order order)
    {
        string companyName = "Pictionary";
        string orderId = order.Id.Value.ToString();

        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        var stream = new MemoryStream();

        var pdfWrite = PdfWriter.GetInstance(pdfDoc, stream);

        pdfDoc.Open();
        pdfDoc.AddAuthor(companyName);
        pdfDoc.AddTitle($"Receipt for Order#{orderId}");
        pdfDoc.Add(new Paragraph($"Receipt for Order#{orderId}", new Font(Font.FontFamily.HELVETICA, 20)));
        pdfDoc.Add(new Paragraph($"Total Price: {order.TotalPrice.ToString()}", new Font(Font.FontFamily.HELVETICA, 20)));
        pdfDoc.Add(Chunk.NEWLINE);
        pdfDoc.Add(new LineSeparator());

        if(order.Polaroids.Any())
        {
            pdfDoc.Add(new Paragraph("List of Purchased Polaroids:"));
        }

        foreach (var (p, i) in order.Polaroids.Select((p, i) => (p, i)))
        {
            pdfDoc.Add(new Paragraph($"          Polaroid Id: {p.Id.Value}"));
            pdfDoc.Add(new Paragraph($"          Polaroid Type: {p.Type.Value}"));
            pdfDoc.Add(new Paragraph($"          Polaroid Quantity: {p.Quantity}"));
            pdfDoc.Add(new Paragraph($"          Polaroid Price: {(p.Type == PolaroidType.Mini ? PolaroidPrices.MINI : PolaroidPrices.WIDE)}"));
            pdfDoc.Add(new Paragraph($"          Total Price: {p.Price}"));
            if(i != order.Polaroids.Count - 1)
            {
                pdfDoc.Add(new Paragraph("          ------------------------------------------------"));
            }
        }
        pdfDoc.Add(Chunk.NEWLINE);
        pdfDoc.Add(new LineSeparator());
        pdfDoc.Add(new Paragraph("FROM PICTIONARY. THANK YOU FOR PURCHASING", new Font() { Size = 15 }));


        
        pdfDoc.Close();

        return Convert.ToBase64String(stream.ToArray());

    }
}