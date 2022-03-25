using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using Vikekh.JsonResume;

namespace Vikekh.Cv.WebRazor.Repositories;

public interface IJsonResumeRepository
{
    public Models.JsonResume Get();
}

public class JsonResumeRepository : IJsonResumeRepository
{
    private readonly IFileProvider _fileProvider;
    private readonly IJsonValidator _jsonValidator;
    private readonly IYamlToJsonConverter _yamlToJsonConverter;

    public JsonResumeRepository(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }

    public Models.JsonResume Get()
    {
        var fileInfo = _fileProvider.GetFileInfo("resume.g.json");

        using (var streamReader = File.OpenText(fileInfo.PhysicalPath))
        using (var jsonTextReader = new JsonTextReader(streamReader))
        {
            var serializer = new Newtonsoft.Json.JsonSerializer();
            return serializer.Deserialize<Models.JsonResume>(jsonTextReader);
        }
    }
}

public class GeneratedJsonResumeRepository : IJsonResumeRepository
{
    private readonly IJsonValidator _jsonValidator;
    private readonly IYamlToJsonConverter _yamlToJsonConverter;

    public GeneratedJsonResumeRepository(IYamlToJsonConverter yamlToJsonConverter, IJsonValidator jsonValidator)
    {
        _yamlToJsonConverter = yamlToJsonConverter;
        _jsonValidator = jsonValidator;
    }

    public Models.JsonResume Get()
    {
        var jsonString = _yamlToJsonConverter.Convert();
        _jsonValidator.Validate(jsonString);

        return JsonConvert.DeserializeObject<Models.JsonResume>(jsonString);
    }
}
