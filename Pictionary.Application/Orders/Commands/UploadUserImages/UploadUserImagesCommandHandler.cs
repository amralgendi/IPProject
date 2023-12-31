using Mediator;
using Pictionary.Application.Orders.Interfaces;

namespace Pictionary.Application.Orders.Commands.UploadUserImages;

public class UploadUserImagesCommandHandler : ICommandHandler<UploadUserImagesCommand>
{
    private readonly IImageRepository _imageRepository;

    public UploadUserImagesCommandHandler(IImageRepository imageRepository)
    {
        _imageRepository = imageRepository;
    }

    public async ValueTask<Unit> Handle(UploadUserImagesCommand command, CancellationToken cancellationToken)
    {
        await _imageRepository.CreateImages(command.userId, command.Images.Select(i => (i.ImageBytes, i.Ext, i.Name)));

        return Unit.Value;
    }
}