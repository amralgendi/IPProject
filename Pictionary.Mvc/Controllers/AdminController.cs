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
    public async Task<IActionResult> Orders()
    {
        var req = new AdminGetOrdersQuery();

        var orders = await _mediator.Send(req);
        return View(orders.ToList());
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
