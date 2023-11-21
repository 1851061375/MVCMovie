using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;

namespace MvcMovie.Controllers;

public class HomeController : Controller
{
    private readonly ISendMailService _sendMailService;
    public HomeController(ISendMailService sendMailService)
    {
        _sendMailService = sendMailService;
    }

    public IActionResult Index()
    {

        return View();
    }

    public IActionResult Error()
    {
        return View("../Shared/Error");
    }

    public async Task<IActionResult> TestMail()
    {

        await _sendMailService.SendEmailAsync("lapphung99@gmail.com", "Kiểm tra thử", "<p><strong>helloo</strong></p>");
        return View("../Shared/RequestInfo");
    }
}
