namespace Pictionary.Mvc.Controllers;

using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pictionary.Application.Addresses.Queries.GetAddresses;
using Pictionary.Application.Auth.Queries.GetUser;
using Pictionary.Application.Orders.Commands.AddOrderDetails;
using Pictionary.Application.Orders.Commands.AdminSetOrderStatus;
using Pictionary.Application.Orders.Commands.CreateOrder;
using Pictionary.Application.Orders.Commands.OrderPaid;
using Pictionary.Application.Orders.Commands.UploadUserImages;
using Pictionary.Application.Orders.Queries.AdminGetOrders;
using Pictionary.Application.Orders.Queries.GetOrder;
using Pictionary.Application.Orders.Queries.GetOrderPaymentLink;
using Pictionary.Application.Orders.Queries.GetOrders;
using Pictionary.Application.Orders.Queries.GetUserImageNames;
using Pictionary.Domain.OrderModel.Entities;
using Pictionary.Domain.OrderModel.ValueObjects;
using Pictionary.Mvc.Models;

public class AdminController : Controller
{
    private readonly ILogger<OrderController> _logger;

    private readonly IWebHostEnvironment _hostingEnvironment;

    private readonly IMediator _mediator;

    public AdminController(ILogger<OrderController> logger, IWebHostEnvironment hostingEnvironment, IMediator mediator)
    {
        _logger = logger;
        _hostingEnvironment = hostingEnvironment;
        _mediator = mediator;
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Orders(int page = 1)
    {
        Console.WriteLine("Page: " + page);
        var req = new AdminGetOrdersQuery(Page: page);

        var orders = await _mediator.Send(req);

        if(!orders.Any())
        {
            return RedirectToAction("orders", 1);
        }
        Console.WriteLine(orders.Count());
        return View(new AdminOrdersViewModel(orders.ToList(), page));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Orders(string id, string status)
    {
        var req = new AdminSetOrderStatusCommand(id, status);

        var _ = await _mediator.Send(req);

        return RedirectToAction("orders");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
