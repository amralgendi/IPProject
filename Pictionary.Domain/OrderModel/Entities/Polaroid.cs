namespace Pictionary.Domain.OrderModel.Entities;

using System.Collections.Generic;
using Pictionary.Domain.Models;
using Pictionary.Domain.OrderModel.Constants;
using Pictionary.Domain.OrderModel.Enums;
using Pictionary.Domain.OrderModel.ValueObjects;

public sealed class Polaroid : Entity<PolaroidId>
{
    private Polaroid(PolaroidId id, PolaroidType type, Image image, int quantity) : base(id)
    {
        Type = type;
        Image = image;
        Quantity = quantity;
        Price = (type.EnumValue == PolaroidTypeEnum.MINI
            ? PolaroidPrices.MINI
            : PolaroidPrices.WIDE) * quantity;
    }

    public PolaroidType Type { get; private set; }

    public decimal Price { get; private set; }

    public Image Image { get; private set; }

    public int Quantity { get; private set; }

    public static Polaroid Create(PolaroidType type, Image image, int quantity)
    {
        return new(PolaroidId.Create(), type, image, quantity);
    }

    #pragma warning disable CS8618
        private Polaroid()
        {
        }
    #pragma warning restore CS8618
}