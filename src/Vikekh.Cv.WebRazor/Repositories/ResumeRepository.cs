using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Serialization;
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

    public void Get()
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

    public void WriteToJson()
    {
        try
        {
            var basicsDto = DeserializeFile<BasicsDto>(Path.Combine(_dataDirectory, "basics.yml"));
            var workDtos = GetFiles(Path.Combine(_dataDirectory, "work")).Select(s => DeserializeFile<WorkDto>(s));
            var educationDtos = GetFiles(Path.Combine(_dataDirectory, "education")).Select(s => DeserializeFile<EducationDto>(s));
            var skillsDtos = GetFiles(Path.Combine(_dataDirectory, "skills")).Select(s => DeserializeFile<SkillsDto>(s));
            var languageDtos = DeserializeFile<IEnumerable<LanguageDto>>(Path.Combine(_dataDirectory, "languages.yml"));

            var dictionary = new Dictionary<string, object>();
            dictionary.Add("basics", basicsDto);
            dictionary.Add("work", workDtos);
            dictionary.Add("education", educationDtos);
            dictionary.Add("skills", skillsDtos);
            dictionary.Add("languages", languageDtos);

            JsonSerializer serializer = new JsonSerializer();
            serializer.ContractResolver = new CamelCasePropertyNamesContractResolver();
            serializer.Formatting = Formatting.Indented;
            //serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter(@"..\..\resume.json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, dictionary);
                // {"ExpiryDate":new Date(1230375600000),"Price":0}
            }

            var json = File.ReadAllText(@"..\..\resume.json");
            var schemaJson = File.ReadAllText(@"..\..\schema.json");
            JsonSchema schema = JsonSchema.Parse(schemaJson);

            JObject person = JObject.Parse(json);
            IList<string> messages;
            bool valid = person.IsValid(schema, out messages);
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
