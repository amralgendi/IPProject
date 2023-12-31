using Pictionary.Domain.Models;

namespace Pictionary.Domain.OrderModel.ValueObjects;

public sealed class Image : ValueObject
{
    private Image(string name, string ext, string data)
    {
        Name = name;
        Ext = ext;
        Data = data;
    }

    public string Name { get; }

    public string Ext { get; }

    public string Data { get; }

    public static Image Parse(string name, string ext, string data)
    {
        return new(name, ext, data);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Ext;
        yield return Data;
    }
}