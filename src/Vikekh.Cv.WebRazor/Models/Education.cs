using Ardalis.GuardClauses;

namespace Vikekh.Cv.WebRazor.Models;

public class Education
{
    public Education(string institution, string studyType, string area, DateTime startDate, DateTime? endDate, Uri? url, decimal? score, IEnumerable<string>? courses)
    {
        Guard.Against.NullOrWhiteSpace(institution, nameof(institution));
        Guard.Against.NullOrWhiteSpace(studyType, nameof(studyType));
    }

    public string Area { get; set; }

    public DateTime? EndDate { get; set; }

    public IEnumerable<string> Courses { get; set; }

    public string Institution { get; set; }

    public decimal? Score { get; set; }

    public DateTime StartDate { get; set; }

    public string StudyType { get; set; }

    public Uri? Url { get; set; }
}
