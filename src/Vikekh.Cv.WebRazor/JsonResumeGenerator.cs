using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NJsonSchema.CodeGeneration.CSharp;
using YamlDotNet.Serialization;

namespace Vikekh.Cv.WebRazor;

public class JsonResumeGenerator
{
    private const string DataDirectory = @"..\..\data";

    public JsonResumeGenerator()
    {
    }

    public async Task RunAsync()
    {
        try
        {
            var json = GetJson();
            var jObject = JObject.Parse(json);

            if (!ValidateJson(jObject)) return;

            WriteJson(jObject);

            var schema = await NJsonSchema.JsonSchema.FromFileAsync(Path.Combine(DataDirectory, "schema.json"));
            var settings = new CSharpGeneratorSettings
            {
                Namespace = "Vikekh.Cv.WebRazor.Models",
                
            };
            var generator = new CSharpGenerator(schema, settings);
            var code = generator.GenerateFile("JsonResume");
            File.WriteAllText(@"Models\JsonResume.g.cs", code);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private static IEnumerable<string> GetFiles(string path)
    {
        if (!Directory.Exists(path)) throw new Exception();

        return Directory.EnumerateFiles(path);
    }

    private static object? DeserializeFile(IDeserializer deserializer, string path)
    {
        if (!File.Exists(path)) throw new Exception();

        using (var streamReader = new StreamReader(path))
        {
            return deserializer.Deserialize(streamReader);
        }
    }

    private string GetJson()
    {
        var deserializer = new DeserializerBuilder().Build();
        var dictionary = new Dictionary<string, object?>()
        {
            { "basics", DeserializeFile(deserializer, Path.Combine(DataDirectory, "basics.yml")) },
            { "work", GetFiles(Path.Combine(DataDirectory, "work")).Select(s => DeserializeFile(deserializer, s)) },
            { "education", GetFiles(Path.Combine(DataDirectory, "education")).Select(s => DeserializeFile(deserializer, s)) },
            { "skills", GetFiles(Path.Combine(DataDirectory, "skills")).Select(s => DeserializeFile(deserializer, s)) },
            { "languages", DeserializeFile(deserializer, Path.Combine(DataDirectory, "languages.yml")) }
        };
        var serializer = new SerializerBuilder()
            .JsonCompatible()
            .Build();

        return serializer.Serialize(dictionary);
    }

    private bool ValidateJson(JObject jObject)
    {
        using (var streamReader = File.OpenText(Path.Combine(DataDirectory, "schema.json")))
        using (var jsonTextReader = new JsonTextReader(streamReader))
        {
            var jSchema = JSchema.Load(jsonTextReader);
            var isValid = jObject.IsValid(jSchema, out IList<ValidationError> validationErrors);

            foreach (var validationError in validationErrors)
            {
                throw new Exception(validationError.Message);
            }

            return isValid;
        }
    }

    private void WriteJson(JObject jObject)
    {
        var serializer = new JsonSerializer
        {
            Converters = { new JavaScriptDateTimeConverter() },
            Formatting = Formatting.Indented
        };

        using (var streamWriter = new StreamWriter(Path.Combine(DataDirectory, "resume.g.json")))
        using (var jsonWriter = new JsonTextWriter(streamWriter))
        {
            serializer.Serialize(jsonWriter, jObject);
        }
    }
}
