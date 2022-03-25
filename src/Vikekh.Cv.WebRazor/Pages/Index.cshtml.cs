using Microsoft.AspNetCore.Mvc.RazorPages;
using Vikekh.Cv.WebRazor.Models;
using Vikekh.Cv.WebRazor.Repositories;

namespace Vikekh.Cv.WebRazor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IJsonResumeRepository _jsonResumeRepository;

        public IndexModel(ILogger<IndexModel> logger, IJsonResumeRepository jsonResumeRepository)
        {
            _logger = logger;
            _jsonResumeRepository = jsonResumeRepository;
        }

        public Models.JsonResume JsonResume { get; private set; }

        public void OnGet()
        {
            JsonResume = _jsonResumeRepository.Get();
        }
    }
}
