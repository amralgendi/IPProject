namespace Pictionary.Application.Orders.Queries.GetUserImageNames;

using System.Threading;
using System.Threading.Tasks;
using Mediator;
using Pictionary.Application.Orders.Interfaces;

public class GetUserImageNamesQueryHandler : IQueryHandler<GetUserImageNamesQuery, IEnumerable<string>>
{
    private readonly IImageRepository _imageRepository;

    public GetUserImageNamesQueryHandler(IImageRepository imageRepository)
    {
        _imageRepository = imageRepository;
    }

    public async ValueTask<IEnumerable<string>> Handle(GetUserImageNamesQuery query, CancellationToken cancellationToken)
    {
        return await _imageRepository.GetImageNames(query.UserId);
    }
}