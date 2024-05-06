using NJsonSchema;
using NJsonSchema.CodeGeneration.CSharp;
using Vikekh.CV.Web.Interfaces;

namespace Vikekh.CV.Web.Services;

public class JsonResumeCsModelGenerator : IJsonResumeCsModelGenerator
{
    public async Task GenerateAsync(string path, string? typeNameHint = null, string? ns = null)
    {
        JsonSchema jsonSchema = await JsonSchema.FromFileAsync(path);
        var cSharpGeneratorSettings = new CSharpGeneratorSettings();

        if (ns != null)
        {
            cSharpGeneratorSettings.Namespace = ns;
        }

        var cSharpGenerator = new CSharpGenerator(jsonSchema, cSharpGeneratorSettings);
        string code;

        if (typeNameHint == null)
        {
            code = cSharpGenerator.GenerateFile();
        }
        else
        {
            code = cSharpGenerator.GenerateFile(typeNameHint);
        }

        File.WriteAllText(path, code);
    }
}