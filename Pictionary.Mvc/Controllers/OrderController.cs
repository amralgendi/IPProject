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
using Pictionary.Application.Orders.Queries.GetOrder;
using Pictionary.Application.Orders.Queries.GetOrderPaymentLink;
using Pictionary.Application.Orders.Queries.GetOrders;
using Pictionary.Application.Orders.Queries.GetUserImageNames;
using Pictionary.Domain.OrderModel.Entities;
using Pictionary.Domain.OrderModel.ValueObjects;
using Pictionary.Mvc.Models;

public class OrderController : Controller
{
    private readonly ILogger<OrderController> _logger;

    private readonly IWebHostEnvironment _hostingEnvironment;

    private readonly IMediator _mediator;

    private readonly string _uploadsDir;

    public OrderController(ILogger<OrderController> logger, IWebHostEnvironment hostingEnvironment, IMediator mediator)
    {
        _logger = logger;
        _hostingEnvironment = hostingEnvironment;
        _mediator = mediator;
        _uploadsDir = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
    }

    [Authorize]
    public IActionResult Index()
    {
        return View(new CreateOrderViewModel());
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Index(CreateOrderViewModel viewModel)
    {
        var id = HttpContext.GetId();

        if(viewModel.PolaroidDetails.Any())
        {
            var req = new CreateOrderCommand(
                id,
                viewModel.PolaroidDetails.Select(p => 
                    new OrderPolaroidCommand(p.Type, p.Quantity, new(p.Ext, p.DataBase64, p.FileName))));

            var order = await _mediator.Send(req);

            return RedirectToAction("checkout",  new { id = order.Id.Value });
        }

        List<(string FileName, string DataBase64, string Ext)> imageDataBase64 = new();

        foreach(var image in viewModel.Images)
        {
            if(image.Length > 2000000)
            {
                throw new Exception("Image file size too big");
            }

            using var stream = new MemoryStream();
            await image.CopyToAsync(stream);
            var ext = Path.GetExtension(image.FileName);
            imageDataBase64.Add((image.FileName.Replace(ext, string.Empty), Convert.ToBase64String(stream.ToArray()), ext[1..]));
        }

        viewModel.PolaroidDetails = imageDataBase64.ConvertAll(d =>
            new PolaroidDetails(d.FileName, 1, PolaroidType.Mini.Value, d.DataBase64, d.Ext));

        return View(viewModel);
    }

    [Authorize]
    public async Task<IActionResult> MyOrder()
    {
        var id = HttpContext.GetId();

        var req = new GetOrdersQuery(id);

        var orders = await _mediator.Send(req);

        return View(orders);
    }

    [Authorize]
    public async Task<IActionResult> Checkout()
    {
        var id = HttpContext.GetId();

        var orderId = RouteData.Values.TryGetValue("id", out var orderIdObj)
            ? orderIdObj!.ToString()
            : null;

        if(orderId is null)
        {
            return RedirectToAction("myorder");
        }

        var orderReq = new GetOrderQuery(id, orderId);
        var order = await _mediator.Send(orderReq);

        var addressesReq = new GetAddressesQuery(id);
        var addresses = await _mediator.Send(addressesReq);

        Console.WriteLine(addresses.Count());

        return View(new CheckoutViewModel(order, addresses));
    }

    [Authorize]
    public async Task<IActionResult> Pay()
    {
        var id = HttpContext.GetId();

        var orderId = RouteData.Values.TryGetValue("id", out var orderIdObj)
            ? orderIdObj!.ToString()
            : null;

        if(orderId is null)
        {
            return RedirectToAction("myorder");
        }

        var orderReq = new GetOrderPaymentLinkQuery(id, orderId);
        var url = await _mediator.Send(orderReq);

        return Redirect(url);
    }

    [Authorize]
    public async Task<IActionResult> Paid()
    {
        var id = HttpContext.GetId();

        var sessionId = Request.Query.TryGetValue("session_id", out var sessionIdQ)
            ? sessionIdQ.ToString()
            : null;

        if(sessionId is null)
        {
            return RedirectToAction("myorder");
        }

        var orderReq = new OrderPaidCommand(id, sessionId);
        var url = await _mediator.Send(orderReq);

        return RedirectToAction("myorder");
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Checkout(CheckoutViewModel viewModel)
    {
        var id = HttpContext.User.FindFirstValue(ClaimTypes.Sid)
            ?? throw new Exception("Not Authorized");

        var orderId = RouteData.Values.TryGetValue("id", out var orderIdObj)
            ? orderIdObj!.ToString()!
            : throw new Exception("No Order Id");

        Console.WriteLine(JsonSerializer.Serialize(viewModel));

        var req = new AddOrderDetailsCommand(
            id,
            orderId,
            new(
                viewModel.AddressRequest.IsExistingAddress,
                viewModel.AddressRequest.IsExistingAddress ? viewModel.AddressRequest.AddressId : null,
                viewModel.AddressRequest.IsExistingAddress ? null : new(
                    viewModel.AddressRequest.AddressName!,
                    viewModel.AddressRequest.FirstName!,
                    viewModel.AddressRequest.LastName!,
                    viewModel.AddressRequest.PhoneNumber!,
                    viewModel.AddressRequest.Street!,
                    viewModel.AddressRequest.HomeNumber!,
                    viewModel.AddressRequest.City!,
                    viewModel.AddressRequest.State!)));

        var order = await _mediator.Send(req);

        return RedirectToAction("pay", new { id = order.Id.Value.ToString() });
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> UploadFiles(List<IFormFile> postedFiles)
    {
        var id = HttpContext.User.FindFirstValue(ClaimTypes.Sid)
            ?? throw new Exception("Not Authorized");

        var userDir = Path.Combine(_uploadsDir, id);

        IEnumerable<(byte[] Bytes, string Ext, string Name)> imagesInfo = postedFiles.Select(f => 
        {
            using var stream = new MemoryStream();
            f.CopyTo(stream);
            var ext = Path.GetExtension(f.FileName);
            return (stream.ToArray(), ext, f.FileName.Replace(ext, ""));
        });

        var req = new UploadUserImagesCommand(
            id,
            imagesInfo.Select(imageInfo => new ImageCommand(imageInfo.Bytes, imageInfo.Ext, imageInfo.Name)));

        await _mediator.Send(req);

        return RedirectToAction("checkout");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
