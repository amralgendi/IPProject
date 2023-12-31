using Pictionary.Application.Orders.Interfaces;

namespace Pictionary.Infrastructure.Persistence.Repositories;

public class ImageRepository : IImageRepository
{
    private readonly string MainDir = Path.Combine(Environment.CurrentDirectory, "uploads");

    public async Task CreateImages(string userId, IEnumerable<(byte[] Bytes, string Ext, string Name)> values)
    {
        await Task.CompletedTask;

        Console.WriteLine(MainDir);

        var userDir = Path.Combine(MainDir, userId);

        if(!Path.Exists(userDir))
        {
            Console.WriteLine("CREATING USER DIRECTORY!");
            Directory.CreateDirectory(userDir);
        }
        else
        {
            Directory.Delete(userDir, true);
            Directory.CreateDirectory(userDir);
        }

        foreach (var (Bytes, Ext, Name) in values)
        {
            string filePath = Path.Combine(userDir, $"{Name}{Ext}");
            while(Path.Exists(filePath))
            {
                filePath = filePath.Replace(Ext, $"1{Ext}");
            }

            await using var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            fs.Write(Bytes, 0, Bytes.Length);
        }
    }

    public async Task<IEnumerable<string>> GetImageNames(string userId)
    {
        await Task.CompletedTask;

        var userDir = Path.Combine(MainDir, userId);

        if(!Path.Exists(userDir))
        {
            return new List<string>();
        }

        return new DirectoryInfo(userDir).GetFiles().Select(f => f.Name);
    }
}