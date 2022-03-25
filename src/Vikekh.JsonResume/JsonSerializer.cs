using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Vikekh.JsonResume;

public interface IJsonSerializer
{
    string Serialize(string jsonString);

    void Serialize(string jsonString, string path);
}

public class JsonSerializer : IJsonSerializer
{
    private readonly Newtonsoft.Json.JsonSerializer _jsonSerializer;

    public JsonSerializer(string schemaPath)
    {
        _jsonSerializer = new Newtonsoft.Json.JsonSerializer
        {
            Formatting = Formatting.Indented
        };
    }

    public string Serialize(string jsonString)
    {
        throw new NotImplementedException();
    }

    public void Serialize(string jsonString, string path)
    {
        var jObject = JObject.Parse(jsonString);

        using (var streamWriter = new StreamWriter(path))
        using (var jsonWriter = new JsonTextWriter(streamWriter))
        {
            _jsonSerializer.Serialize(jsonWriter, jObject);
        }
    }
}
