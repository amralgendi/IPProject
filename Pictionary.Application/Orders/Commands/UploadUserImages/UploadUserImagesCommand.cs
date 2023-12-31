namespace Pictionary.Application.Orders.Commands.UploadUserImages;

using Mediator;

public record UploadUserImagesCommand(
    string userId,
    IEnumerable<ImageCommand> Images) : ICommand;

public record ImageCommand(byte[] ImageBytes, string Ext, string Name);