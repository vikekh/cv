using Microsoft.Extensions.FileProviders;
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
        _dataPath = @"data/yaml";

        //if (!Directory.Exists(_dataPath)) throw new Exception($"Directory `{_dataPath}` does not exist.");

        _deserializer = new DeserializerBuilder().Build();
        _serializer = new SerializerBuilder().JsonCompatible().Build();
    }

    public string Convert()
    {
        var dictionary = new Dictionary<string, object?>();

        foreach (var fileInfo in _fileProvider.GetDirectoryContents(_dataPath))
        {
            if (fileInfo.IsDirectory)
            {
                dictionary.Add(fileInfo.Name, GetList(fileInfo.Name));
            }
            else
            {
                var yamlFile = GetYamlFile(fileInfo);

                if (yamlFile == null) continue;

                dictionary.Add(yamlFile.Name, yamlFile.Deserialize(_deserializer));
            }
        }

        return _serializer.Serialize(dictionary);
    }

    private IEnumerable<object?> GetList(string directoryName)
    {
        foreach (var fileInfo in _fileProvider.GetDirectoryContents(Path.Combine(_dataPath, directoryName)))
        {
            yield return GetYamlFile(fileInfo)?.Deserialize(_deserializer);
        }
    }

    private YamlFile? GetYamlFile(IFileInfo fileInfo)
    {
        try
        {
            return new YamlFile(fileInfo);
        }
        catch
        {
            return null;
        }
    }

    private class YamlFile
    {
        public YamlFile(IFileInfo fileInfo)
        {
            FileInfo = fileInfo;
            
            if (FileInfo.IsDirectory) throw new NotSupportedException("Cannot be a directory.");

            var nameParts = FileInfo.Name.Split('.');

            if (nameParts.Length < 2 || nameParts.Length > 3) throw new NotSupportedException("File name does not follow specification.");

            Extension = nameParts[nameParts.Length - 1];

            if (!HasYamlExtension) throw new NotSupportedException("File does not have YAML extension.");

            Name = nameParts[0];
            Tag = nameParts.Length == 3 ? nameParts[1] : null;
        }

        public string Extension { get; private set; }

        public IFileInfo FileInfo { get; private set; }

        private bool HasYamlExtension => Extension == "yml" || Extension == "yaml";

        public string Name { get; private set; }

        public string? Tag { get; private set; }

        public object? Deserialize(IDeserializer deserializer)
        {
            using (var streamReader = new StreamReader(FileInfo.PhysicalPath))
            {
                return deserializer.Deserialize(streamReader);
            }
        }
    }
}
