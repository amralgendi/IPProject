namespace Pictionary.Application.Orders.Interfaces;

public interface IImageRepository
{
    Task<IEnumerable<string>> GetImageNames(string UserId);

    Task CreateImages(string UserId, IEnumerable<(byte[] Bytes, string Ext, string Name)> images);
}