using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using YamlDotNet.Serialization;

namespace Vikekh.Cv.WebRazor;

public class YamlToJsonConverter
{
    private readonly string _dataPath;
    private readonly IDeserializer _deserializer;
    private readonly IFileProvider _fileProvider;
    private readonly ISerializer _serializer;

    public YamlToJsonConverter(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
        _dataPath = @"..\..\data\yaml";

        if (!Directory.Exists(_dataPath)) throw new Exception($"Directory `{_dataPath}` does not exist.");

        _deserializer = new DeserializerBuilder().Build();
        _serializer = new SerializerBuilder().JsonCompatible().Build();
    }

    public string Convert()
    {
        var dictionary = new Dictionary<string, object?>();

        var directoryContents = _fileProvider.GetDirectoryContents(_dataPath);

        foreach (var path in Directory.GetFiles(_dataPath))
        {
            if (!YamlFile.TryCreate(path, out YamlFile? yamlFile)) continue;

            dictionary.Add(yamlFile.Name, yamlFile.Deserialize(_deserializer));
        }

        foreach (var path in Directory.GetDirectories(_dataPath))
        {
            var name = Path.GetFileName(path) ?? throw new Exception("Could not get directory name.");
            var list = new List<object?>();

            foreach (var filePath in Directory.GetFiles(path))
            {
                if (!YamlFile.TryCreate(filePath, out YamlFile? yamlFile)) continue;

                list.Add(yamlFile.Deserialize(_deserializer));
            }

            dictionary.Add(name, list);
        }

        return _serializer.Serialize(dictionary);
    }

    private class YamlFile
    {
        public YamlFile(string path)
        {
            Path = path;
            FileName = System.IO.Path.GetFileName(path);
            var parts = FileName.Split('.');

            if (parts.Length < 2 || parts.Length > 3) throw new NotSupportedException("File name does not follow specification.");

            Extension = parts[parts.Length - 1];

            if (!HasYamlExtension) throw new NotSupportedException("File does not have YAML extension.");

            Name = parts[0];
            Tag = parts.Length == 3 ? parts[1] : null;
        }

        public string Extension { get; private set; }

        public string FileName { get; private set; }

        private bool HasYamlExtension => Extension == "yml" || Extension == "yaml";

        public string Name { get; private set; }

        public string Path { get; private set; }

        public string? Tag { get; private set; }

        public object? Deserialize(IDeserializer deserializer)
        {
            using (var streamReader = new StreamReader(Path))
            {
                return deserializer.Deserialize(streamReader);
            }
        }

        public static bool TryCreate(string path, out YamlFile? yamlFile)
        {
            yamlFile = null;

            try
            {
                yamlFile = new YamlFile(path);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
