namespace Pictionary.Mvc.Controllers;

using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json;
using Mediator;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pictionary.Application.Auth.Commands.SignUp;
using Pictionary.Application.Auth.Queries.GetUser;
using Pictionary.Application.Auth.Queries.LogIn;
using Pictionary.Contracts.Auth;
using Pictionary.Domain.UserModel;
using Pictionary.Mvc.Models;

public class AuthController : Controller
{
    private readonly ILogger<AuthController> _logger;

    private readonly IMediator _mediator;

    public AuthController(ILogger<AuthController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public IActionResult Index()
    {
        return RedirectToAction("signup");
    }

    [Authorize]
    public async Task<IActionResult> AuthUser()
    {
        var id = User.FindFirstValue(ClaimTypes.Sid);

        if(id is null)
        {
            return RedirectToAction("login");
        }

        var req = new GetUserQuery(id);

        try
        {
            var user = await _mediator.Send(req);
            return View(user);
        }
        catch
        {
            return RedirectToAction("logout", new { });
        }
    }

    public IActionResult SignUp()
    {
        var email = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if(email is not null)
        {
            return RedirectToAction("authuser");
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpRequest request)
    {
        Console.WriteLine(JsonSerializer.Serialize(request));

        var command = new SignUpCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.PhoneNumber,
            request.Password,
            request.ConfirmPassword);

        await _mediator.Send(command);

        return View("login");
    }

    public IActionResult LogIn()
    {
        var email = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if(email is not null)
        {
            return RedirectToAction("authuser");
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> LogIn(LogInRequest request)
    {
        Console.WriteLine(request);

        var query = new LogInQuery(request.Email, request.Password);

        var user = await _mediator.Send(query);

        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, user.Email),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Sid, user.Id.Value.ToString()),
            new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new(ClaimTypes.GivenName, user.Id.Value.ToString()),
            new(ClaimTypes.GivenName, user.FirstName),
            new(ClaimTypes.Surname, user.LastName),
            new(ClaimTypes.HomePhone, user.PhoneNumber),
            new(ClaimTypes.Role, user.Role),
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            new AuthenticationProperties()
            {
                IsPersistent = true,
            });

        return RedirectToAction("", "");
    }

    public async Task<IActionResult> LogOut()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("", "home");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
