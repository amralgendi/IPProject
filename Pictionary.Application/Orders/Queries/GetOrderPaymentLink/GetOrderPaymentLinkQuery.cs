using Mediator;

namespace Pictionary.Application.Orders.Queries.GetOrderPaymentLink;

public record GetOrderPaymentLinkQuery(string UserId, string OrderId) : IQuery<string>;