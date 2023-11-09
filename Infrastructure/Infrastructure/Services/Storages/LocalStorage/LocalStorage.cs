using Application.Abstraction.Storage.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.Storages.LocalStorage
{
    public class LocalStorage : Storage, ILocalStorage
    {
        private readonly IWebHostEnvironment _env;

        public LocalStorage(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task DeleteAsync(string pathOrContainerName, string fileName)
            => File.Delete($"{pathOrContainerName}\\{fileName}");
        

        public List<string> GetAllFiles(string pathOrContainerName)
        {
            DirectoryInfo directoryInfo = new(pathOrContainerName);
            return directoryInfo.GetFiles().Select(x => x.Name).ToList();
        }

        public bool HasFile(string pathOrContainerName, string fileName)
        {
            return File.Exists($"{pathOrContainerName}\\{fileName}");
        }

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection formFiles)
        {
            string uploadPath = Path.Combine(_env.WebRootPath, pathOrContainerName);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            List<(string fileName, string path)> datas = new();


            foreach (var file in formFiles)
            {
                var newFileName = await FileRenameAsync(uploadPath, file.FileName, HasFile);
                await CopyFileAsync($"{uploadPath}\\{newFileName}", file);
                datas.Add((newFileName, $"{uploadPath}\\{newFileName}"));
            }

            // todo Custom Exception!
            return datas;
        }


        async Task<bool> CopyFileAsync(string filePath, IFormFile formFile)
        {
            try
            {
                await using FileStream fileStream = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await formFile.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception)
            {
                // todo Log!
                return false;
            }
        }
    }
}
