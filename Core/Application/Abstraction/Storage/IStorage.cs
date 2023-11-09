using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstraction.Storage
{
    public interface IStorage
    {
        Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection formFiles);
        Task DeleteAsync(string pathOrContainerName, string fileName);
        List<string> GetAllFiles(string pathOrContainerName);
        bool HasFile(string pathOrContainerName, string fileName);
    }
}
