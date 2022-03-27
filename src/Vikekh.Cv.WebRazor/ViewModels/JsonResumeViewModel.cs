namespace Vikekh.Cv.WebRazor.ViewModels;

public class JsonResumeViewModel
{
    public JsonResumeViewModel(Models.JsonResume jsonResume)
    {
        Email = jsonResume.Basics.Email;
        Name = jsonResume.Basics.Name;
        PhoneNumber = jsonResume.Basics.Phone;
        EducationItems = jsonResume.Education.Select(e => new EducationItemViewModel(e));
        SkillsItems = jsonResume.Skills.Select(s => new SkillsItemViewModel(s));
        WorkItems = jsonResume.Work.Select(w => new WorkItemViewModel(w));
    }

    public IEnumerable<EducationItemViewModel> EducationItems { get; private set; }

    public string Email { get; private set; }

    public string Name { get; private set; }

    public string PhoneNumber { get; private set; }

    public IEnumerable<SkillsItemViewModel> SkillsItems { get; private set; }

    public IEnumerable<WorkItemViewModel> WorkItems { get; private set; }
}

public class WorkItemViewModel
{
    public WorkItemViewModel(Models.Work work)
    {
        Description = work.Summary;
        if (DateTime.TryParse(work.EndDate, out DateTime endDate)) EndDate = endDate;
        Location = work.Location;
        Name = work.Name;
        Position = work.Position;
        StartDate = DateTime.Parse(work.StartDate);
    }

    public string Description { get; private set; }

    public DateTime? EndDate { get; private set; }

    public string Location { get; private set; }

    public string Name { get; private set; }

    public string Position { get; private set; }

    public DateTime StartDate { get; private set; }
}

public class EducationItemViewModel
{
    public EducationItemViewModel(Models.Education education)
    {
        Area = education.Area;
        EndDate = DateTime.Parse(education.EndDate);
        Name = education.Institution;
        StartDate = DateTime.Parse(education.StartDate);
    }

    public string Area { get; private set; }

    public DateTime? EndDate { get; private set; }

    public string Name { get; private set; }

    public DateTime StartDate { get; private set; }
}

public class SkillsItemViewModel
{
    public SkillsItemViewModel(Models.Skills skills)
    {
        Name = skills.Name;
        Keywords = skills.Keywords;
    }

    public string Name { get; private set; }

    public IEnumerable<string> Keywords { get; private set; }
}
