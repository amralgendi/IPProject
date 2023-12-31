using System.Text.Json;
using Pictionary.Application.Orders.Interfaces;
using Pictionary.Domain.OrderModel;
using Pictionary.Domain.OrderModel.ValueObjects;
using Pictionary.Domain.UserModel;
using Pictionary.Domain.UserModel.ValueObjects;
using Stripe;
using Stripe.Checkout;

namespace Pictionary.Infrastructure.Services;

public class StripeService : IPaymentService
{
    private const string SECRET = "sk_test_51LaVGBFqgpEkAcuY3QHHF576YMLDV4NMRPC1YPtm5CwxL7w4q47ULl0iJHa3TGkQzmAwfqYmkWHEUzeSdt0qthlb00KTLAiKNo";

    private const string MINI_PRICE_ID = "price_1OTMEzFqgpEkAcuY7qFOC2lz";

    private const string WIDE_PRICE_ID = "price_1OTGBlFqgpEkAcuYmqdoFHxH";

    private readonly SessionService _sessionService;

    private readonly CustomerService _customerService;

    public StripeService()
    {
        _sessionService = new SessionService();
        _customerService = new CustomerService();
        StripeConfiguration.ApiKey = SECRET;
    }

    public async Task<string> GeneratePaymentLink(Order order, User user)
    {
        var customer = await GetCustomer(user);

        var lineItems = new List<SessionLineItemOptions>();

        if(order.Polaroids.Any(p => p.Type == PolaroidType.Mini))
        {
            lineItems.Add(new SessionLineItemOptions
            {
                Price = MINI_PRICE_ID,
                Quantity =  order.Polaroids.Sum(p => p.Type == PolaroidType.Mini ? p.Quantity : 0),
            });
        }

        if(order.Polaroids.Any(p => p.Type == PolaroidType.Wide))
        {
            lineItems.Add(new SessionLineItemOptions()
            {
                Price = WIDE_PRICE_ID,
                Quantity =  order.Polaroids.Sum(p => p.Type == PolaroidType.Wide ? p.Quantity : 0),
            });
        }

        // Console.WriteLine(JsonSerializer.Serialize(Environment.GetEnvironmentVariables()));

        var options = new SessionCreateOptions
        {
            LineItems = lineItems,
            Mode = "payment",
            SuccessUrl = Environment.GetEnvironmentVariable("ASPNETCORE_URLS") + "/order/paid?success=true&session_id={CHECKOUT_SESSION_ID}",
            CancelUrl = Environment.GetEnvironmentVariable("ASPNETCORE_URLS") + "/order/paid?canceled=true",
            Customer = customer.Id,
            Metadata = new()
            {
                { "OrderId", order.Id.Value.ToString() },
            },
        };

        var res = await _sessionService.CreateAsync(options);

        return res.Url;
    }

    public async Task<(OrderId OrderId, bool IsSuccess)> GetDataFromCheckoutId(string sessionId)
    {
        var session = await _sessionService.GetAsync(sessionId);

        Console.WriteLine(session.Status);

        var orderId = OrderId.Parse(session.Metadata["OrderId"]);
        return (orderId, session.Status == "complete");
    }

    private async Task<Customer> GetCustomer(User user)
    {
        var searchOptions = new CustomerSearchOptions()
        {
            Query = $"metadata['UserId']:'{user.Id.Value}'"
        };

        var customers = await _customerService.SearchAsync(searchOptions);

        if(customers.Any())
        {
            return customers.First();
        }

        var createOptions = new CustomerCreateOptions()
        {
            Name = $"{user.FirstName} {user.LastName}",
            Email = user.Email,
            Phone = user.PhoneNumber,
            Metadata = new()
            {
                { "UserId", user.Id.Value.ToString() },
            },
        };

        return await _customerService.CreateAsync(createOptions);
    }
}