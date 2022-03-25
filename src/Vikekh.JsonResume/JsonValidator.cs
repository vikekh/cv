using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace Vikekh.JsonResume;

public interface IJsonValidator
{
    bool Validate(string jsonString);
}

public class JsonValidator : IJsonValidator
{
    private readonly JSchema _jSchema;

    public JsonValidator(string schemaPath)
    {
        _jSchema = GetJSchema(schemaPath);
    }

    public bool Validate(string jsonString)
    {
        var jObject = JObject.Parse(jsonString);
        var isValid = jObject.IsValid(_jSchema, out IList<ValidationError> validationErrors);

        foreach (var validationError in validationErrors)
        {
            throw new Exception(validationError.Message);
        }

        return isValid;
    }

    private static JSchema GetJSchema(string path)
    {
        using (var streamReader = File.OpenText(path))
        using (var jsonTextReader = new JsonTextReader(streamReader))
        {
            return JSchema.Load(jsonTextReader);
        }
    }
}
