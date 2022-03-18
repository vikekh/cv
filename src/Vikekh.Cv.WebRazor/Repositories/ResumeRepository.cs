using Newtonsoft.Json;
using Vikekh.Cv.WebRazor.Models;

namespace Vikekh.Cv.WebRazor.Repositories;

public class ResumeRepository
{
    private const string DataDirectory = @"..\..\data";

    public ResumeRepository()
    {
    }

    public JsonResume Get()
    {
        using (var streamReader = File.OpenText(Path.Combine(DataDirectory, "resume.g.json")))
        using (var jsonTextReader = new JsonTextReader(streamReader))
        {
            var serializer = new JsonSerializer();
            return serializer.Deserialize<JsonResume>(jsonTextReader);
        }
    }
}
