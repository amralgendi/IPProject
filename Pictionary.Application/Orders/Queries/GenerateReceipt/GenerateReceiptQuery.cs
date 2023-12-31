using Mediator;

namespace Pictionary.Application.Orders.Queries.GenerateReceipt;

public record GenerateReceiptQuery(string UserId, string OrderId, string UserRole) : IQuery<string>;