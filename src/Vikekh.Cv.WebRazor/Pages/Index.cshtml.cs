using Microsoft.AspNetCore.Mvc.RazorPages;
using Vikekh.Cv.WebRazor.Models;
using Vikekh.Cv.WebRazor.Repositories;

namespace Vikekh.Cv.WebRazor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ResumeRepository _resumeRepository;

        public IndexModel(ILogger<IndexModel> logger, ResumeRepository resumeRepository)
        {
            _logger = logger;
            _resumeRepository = resumeRepository;
        }

        public JsonResume Resume { get; private set; }

        public void OnGet()
        {
            Resume = _resumeRepository.Get();
        }
    }
}
