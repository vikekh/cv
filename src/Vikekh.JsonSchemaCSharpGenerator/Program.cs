// See https://aka.ms/new-console-template for more information
using NJsonSchema;
using NJsonSchema.CodeGeneration.CSharp;

string jsonSchemaPath = args[0];
string destinationPath = args[1];
string? ns = null;
string? typeNameHint = null;

if (args.Length > 2)
{
    ns = args[2];
}

if (args.Length > 3)
{
    typeNameHint = args[3];
}

JsonSchema jsonSchema = await JsonSchema.FromFileAsync(jsonSchemaPath);
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

File.WriteAllText(destinationPath, code);
