using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Vikekh.Cv.Core.Interfaces;
using Vikekh.Cv.Web.Models;

namespace Vikekh.Cv.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IResumeRepository _resumeRepository;

        public HomeController(IResumeRepository resumeRepository, ILogger<HomeController> logger)
        {
            _resumeRepository = resumeRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = _resumeRepository.GetResume();
            var viewModel = new ResumeViewModel(model);
            return View(viewModel);
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
