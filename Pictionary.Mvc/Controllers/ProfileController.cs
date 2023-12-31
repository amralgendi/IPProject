namespace Pictionary.Mvc.Controllers;

using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json;
using Mediator;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pictionary.Application.Addresses.Commands.DeleteAddress;
using Pictionary.Application.Addresses.Queries.GetAddresses;
using Pictionary.Application.Auth.Commands.SignUp;
using Pictionary.Application.Auth.Queries.GetUser;
using Pictionary.Application.Auth.Queries.LogIn;
using Pictionary.Contracts.Auth;
using Pictionary.Domain.UserModel;
using Pictionary.Mvc.Models;

public class ProfileController : Controller
{
    private readonly ILogger<ProfileController> _logger;

    private readonly IMediator _mediator;

    public ProfileController(ILogger<ProfileController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        var id = HttpContext.GetId();

        var userReq = new GetUserQuery(id);
        var addressReq = new GetAddressesQuery(id);

        var user = await _mediator.Send(userReq);
        var addresses = await _mediator.Send(addressReq);
        return View((user, addresses.ToList()));
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Address()
    {
        var id = HttpContext.GetId();

        var addressId = RouteData.Values.TryGetValue("id", out var orderIdObj)
            ? orderIdObj!.ToString()!
            : null;

        if(addressId is not null)
        {
            var req = new DeleteAddressCommand(id, addressId);
            await _mediator.Send(req);
        }

        return RedirectToAction("");
    }
}
