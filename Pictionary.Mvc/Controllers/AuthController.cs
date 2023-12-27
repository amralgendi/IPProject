namespace Pictionary.Mvc.Controllers;

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Pictionary.Mvc.Models;

public class AuthController : Controller
{
    private readonly ILogger<AuthController> _logger;

    public AuthController(ILogger<AuthController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return RedirectToAction("signup");
    }

    public IActionResult SignUp()
    {
        return View();
    }
    
    public IActionResult LogIn()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
