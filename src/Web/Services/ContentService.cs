using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vikekh.CV.Web.Interfaces.Repositories;
using Vikekh.CV.Web.Interfaces.Services;

namespace Vikekh.CV.Web.Services
{
    public class ContentService : IContentService
    {
        private readonly IContentRepository _contentRepository;

        public ContentService(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        public string GetByPath(string path)
        {
            throw new NotImplementedException();
        }
    }
}
