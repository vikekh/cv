using Vikekh.Cv.WebRazor.Dtos;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Vikekh.Cv.WebRazor.Repositories;

public class ResumeRepository
{
    private readonly string _dataDirectory = @"..\..\data";
    private readonly IDeserializer _deserializer;

    public ResumeRepository()
    {
        _deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
    }

    public void GetResume()
    {
        try
        {
            var basicsDto = DeserializeFile<BasicsDto>(Path.Combine(_dataDirectory, "basics.yml"));
            var workDtos = GetFiles(Path.Combine(_dataDirectory, "work")).Select(s => DeserializeFile<WorkDto>(s));
            var educationDtos = GetFiles(Path.Combine(_dataDirectory, "education")).Select(s => DeserializeFile<EducationDto>(s));
            var skillsDtos = GetFiles(Path.Combine(_dataDirectory, "skills")).Select(s => DeserializeFile<SkillsDto>(s));
            var languageDtos = DeserializeFile<IEnumerable<LanguageDto>>(Path.Combine(_dataDirectory, "languages.yml"));
        }
        catch (Exception ex)
        {
            throw new Exception("Exception thrown.", ex);
        }
    }

    private static IEnumerable<string> GetFiles(string path)
    {
        if (!Directory.Exists(path)) throw new Exception();

        return Directory.EnumerateFiles(path);
    }

    private T DeserializeFile<T>(string path)
    {
        if (!File.Exists(path)) throw new Exception();

        var yaml = File.ReadAllText(path);
        return _deserializer.Deserialize<T>(yaml);
    }
}
