using Pictionary.Domain.OrderModel.Constants;
using Pictionary.Domain.OrderModel.ValueObjects;

namespace Pictionary.Mvc.Models;

public class CreateOrderViewModel
{
    public CreateOrderViewModel()
    {
        Images = new();
        PolaroidDetails = new();
    }

    public List<IFormFile> Images { get; set; }

    public List<PolaroidDetails> PolaroidDetails { get; set; }

    public List<(string Type, decimal Price)> TypeToPrice = new() { (PolaroidType.Mini.Value, PolaroidPrices.MINI), (PolaroidType.Wide.Value, PolaroidPrices.WIDE) };
}

public record PolaroidDetails(string FileName, int Quantity, string Type, string DataBase64, string Ext);