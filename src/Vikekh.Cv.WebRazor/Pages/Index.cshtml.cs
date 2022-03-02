using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vikekh.Cv.WebRazor.Repositories;

namespace Vikekh.Cv.WebRazor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, ResumeRepository resumeRepository)
        {
            _logger = logger;
            resumeRepository.GetResume();
        }

        public void OnGet()
        {

        }
    }
}
