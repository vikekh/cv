using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Vikekh.Cv.Web.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Vikekh.Cv.Web.Repositories
{
    public class FileRepository
    {
        public async Task<Basics> GetBasicsAsync()
        {
            using (var reader = File.OpenText(@"..\..\data\basics.yml"))
            {
                var yaml = await reader.ReadToEndAsync();
                var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .Build();
                var basics = deserializer.Deserialize<Basics>(yaml);
                return basics;
            }
        }
    }
}
