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
    public class TextToSpeechController : Controller
    {
        private readonly ILogger<TextToSpeechController> _logger;

        public TextToSpeechController(
            ILogger<TextToSpeechController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}