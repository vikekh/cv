using System.IO;
using Vikekh.Cv.Core.Interfaces;
using Vikekh.Cv.Core.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Vikekh.Cv.Infrastructure
{
    public class ResumeRepository : IResumeRepository
    {
        public ResumeRepository() {}

        public Resume GetResume()
        {
            using (var reader = new StreamReader(@"..\..\_data\resume.yml"))
            {
                var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .Build();

                return deserializer.Deserialize<Resume>(reader);
            }
            
        }
    }
}
