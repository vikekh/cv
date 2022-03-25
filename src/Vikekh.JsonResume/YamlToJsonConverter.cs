using YamlDotNet.Serialization;

namespace Vikekh.JsonResume;

public interface IYamlToJsonConverter
{
    string Convert();
}

public class YamlToJsonConverter : IYamlToJsonConverter
{
    private readonly string _root;
    private readonly IDeserializer _deserializer;
    private readonly ISerializer _serializer;

    public YamlToJsonConverter(string root)
    {
        _root = root;
        _deserializer = new DeserializerBuilder().Build();
        _serializer = new SerializerBuilder().JsonCompatible().Build();
    }

    public string Convert()
    {
        var dictionary = new Dictionary<string, object?>();

        foreach (var path in Directory.GetFiles(_root))
        {
            if (!YamlFile.TryCreate(path, out YamlFile? yamlFile)) continue;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            dictionary.Add(yamlFile.Name, yamlFile.Deserialize(_deserializer));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        foreach (var path in Directory.GetDirectories(_root))
        {
            dictionary.Add(Path.GetFileName(path), GetList(path));
        }

        return _serializer.Serialize(dictionary);
    }

    private IEnumerable<object?> GetList(string directoryPath)
    {
        foreach (var path in Directory.GetFiles(directoryPath))
        {
            if (!YamlFile.TryCreate(path, out YamlFile? yamlFile)) continue;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            yield return yamlFile.Deserialize(_deserializer);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
    }

    private class YamlFile
    {
        public YamlFile(string path)
        {
            Path = path;

            if (!HasYamlExtension) throw new NotSupportedException("File does not have YAML extension.");
        }

        public string Extension => System.IO.Path.GetExtension(Path);

        private bool HasYamlExtension => Extension == ".yml" || Extension == ".yaml";

        public string Name => System.IO.Path.GetFileNameWithoutExtension(Path);

        public string Path { get; private set; }

        public static bool TryCreate(string path, out YamlFile? yamlFile)
        {
            yamlFile = null;

            try
            {
                yamlFile = new YamlFile(path);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public object? Deserialize(IDeserializer deserializer)
        {
            using (var streamReader = new StreamReader(Path))
            {
                return deserializer.Deserialize(streamReader);
            }
        }
    }
}
