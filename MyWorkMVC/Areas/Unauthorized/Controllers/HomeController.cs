using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyWorkMVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkMVC.Areas.Unauthorized.Controllers
{
    [Area("Unauthorized")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger,
                              SignInManager<UserModel> signInManager,
                              IConfiguration config)
        {
            _logger = logger;
            _signInManager = signInManager;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> DemoClientLogin()
        {
            var returnUrl = Url.Content("~/Client/Home");

            var result = await _signInManager.PasswordSignInAsync("democlient@mailinator.com", _config["Credentials:DemoClient"], false, false);
            _logger.LogInformation("Demo Client logged in.");
            return LocalRedirect(returnUrl);
        }

        [HttpPost]
        public async Task<IActionResult> DemoFreelancerLogin()
        {
            var returnUrl = Url.Content("~/Freelancer/Home");

            var result = await _signInManager.PasswordSignInAsync("demofreelancer@mailinator.com", _config["Credentials:DemoFreelancer"], false, false);
            _logger.LogInformation("Demo Freelancer logged in.");
            return LocalRedirect(returnUrl);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
