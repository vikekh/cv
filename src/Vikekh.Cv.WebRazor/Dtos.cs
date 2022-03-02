namespace Vikekh.Cv.WebRazor.Dtos;

public class AwardDto
{
    public string? Awarder { get; set; }

    public DateTime? Date { get; set; }

    public string? Summary { get; set; }

    public string? Title { get; set; }
}

public class BasicsDto
{
    public string? Email { get; set; }

    public Uri? Image { get; set; }

    public string? Label { get; set; }

    public LocationDto? Location { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public ProfileDto[]? Profiles { get; set; }

    public string? Summary { get; set; }

    public Uri? Url { get; set; }
}

public class EducationDto
{
    public string? Area { get; set; }

    public DateTime? EndDate { get; set; }

    public string[]? Courses { get; set; }

    public string? Institution { get; set; }

    public decimal? Score { get; set; }

    public DateTime? StartDate { get; set; }

    public string? StudyType { get; set; }

    public Uri? Url { get; set; }
}

public class InterestDto
{
    public string[]? Keywords { get; set; }

    public string? Name { get; set; }
}

public class LanguageDto
{
    public string? Fluency { get; set; }

    public string? Language { get; set; }
}

public class LocationDto
{
    public string? Address { get; set; }

    public string? City { get; set; }

    public string? CountryCode { get; set; }

    public string? PostalCode { get; set; }

    public string? Region { get; set; }
}

public class ProfileDto
{
    public string? Network { get; set; }

    public Uri? Url { get; set; }

    public string? Username { get; set; }
}

public class ProjectDto
{
    public string? Description { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Entity { get; set; }

    public string[]? Highlights { get; set; }

    public string[]? Keywords { get; set; }

    public string? Name { get; set; }

    public string[]? Roles { get; set; }

    public DateTime? StartDate { get; set; }

    public string? Type { get; set; }

    public Uri? Url { get; set; }
}

public class PublicationDto
{
    public string? Name { get; set; }

    public string? Publisher { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public string? Summary { get; set; }

    public Uri? Url { get; set; }
}

public class ReferenceDto
{
    public string? Name { get; set; }

    public string? Reference { get; set; }
}

public class SkillsDto
{
    public string[]? Keywords { get; set; }

    public string? Level { get; set; }

    public string? Name { get; set; }
}

public class VolunteerDto
{
    public DateTime? EndDate { get; set; }

    public string[]? Highlights { get; set; }

    public string? Organization { get; set; }

    public string? Position { get; set; }

    public DateTime? StartDate { get; set; }

    public string? Summary { get; set; }

    public Uri? Url { get; set; }
}

public class WorkDto
{
    public string? Description { get; set; }

    public DateTime? EndDate { get; set; }

    public string[]? Highlights { get; set; }

    public string? Location { get; set; }

    public string? Name { get; set; }

    public string? Position { get; set; }

    public DateTime? StartDate { get; set; }

    public string? Summary { get; set; }

    public Uri? Url { get; set; }
}
