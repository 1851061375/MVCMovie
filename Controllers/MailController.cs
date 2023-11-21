using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    [ActivatorUtilitiesConstructor]
    public class MailController : Controller
    {
        private readonly ILogger<MailController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ISendMailService _sendMailService;
        public MailController(
            ILogger<MailController> logger,
            IConfiguration configuration,
            ISendMailService sendMailService)
        {
            _logger = logger;
            _configuration = configuration;
            _sendMailService = sendMailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Compose()
        {
            var _mailSettings = _configuration.GetSection("MailSettings").Get<MailSettings>();
            ViewData["Mail"] = _mailSettings.Mail;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Compose(MailContent mailContent)
        {
            if (ModelState.IsValid)
            {
                await _sendMailService.SendEmailAsync(mailContent.To, mailContent.Subject, mailContent.Body);
            }
            return View("./Compose");
        }

    }
}