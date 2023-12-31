namespace Pictionary.Domain.OrderModel.ValueObjects;

using Pictionary.Domain.Models;
using Pictionary.Domain.OrderModel.Enums;

public class PolaroidType : ValueObject
{
    private PolaroidType(PolaroidTypeEnum type)
    {
        Value = type.ToString();
        EnumValue = type;
    }

    public string Value { get; }

    public PolaroidTypeEnum EnumValue { get; }

    public static PolaroidType Mini => Parse(PolaroidTypeEnum.MINI);

    public static PolaroidType Wide => Parse(PolaroidTypeEnum.WIDE);

    public static PolaroidType Parse(string type)
    {
        if (!Enum.TryParse(type, out PolaroidTypeEnum enumType))
        {
            throw new Exception("Can't Parse");
        }

        return new(enumType);
    }

    public static PolaroidType Parse(PolaroidTypeEnum type)
    {
        return new(type);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}