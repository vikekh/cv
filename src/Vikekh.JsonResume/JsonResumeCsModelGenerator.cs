using NJsonSchema;
using NJsonSchema.CodeGeneration.CSharp;

namespace Vikekh.JsonResume;

public interface IJsonResumeCsModelGenerator
{
    Task GenerateAsync(string path, string? typeNameHint = null, string? ns = null);
}

public class JsonResumeCsModelGenerator : IJsonResumeCsModelGenerator
{
    public async Task GenerateAsync(string path, string? typeNameHint = null, string? ns = null)
    {
        var jsonSchema = await JsonSchema.FromFileAsync(path);
        var cSharpGenerator = new CSharpGenerator(jsonSchema, new CSharpGeneratorSettings
        {
            Namespace = ns
        });
        var code = cSharpGenerator.GenerateFile(typeNameHint);
        File.WriteAllText(path, code);
    }
}
