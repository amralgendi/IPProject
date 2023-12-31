namespace Pictionary.Application.Orders.Queries.GetUserImageNames;

using Mediator;

public record GetUserImageNamesQuery(string UserId) : IQuery<IEnumerable<string>>;